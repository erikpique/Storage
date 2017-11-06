using NUnit.Framework;
using Storage.Core.Exceptions;
using Storage.Core.Providers;
using Storage.Core.Providers.Interfaces;
using Storage.Core.Strategies;
using Storage.Core.Strategies.Interfaces;
using Storage.Core.Test.Models;
using System;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;

namespace Storage.Core.Test
{
    public class MemoryCacheStorageTest
    {
        private IContextStorageProvider<MemoryCache> _storage;
        private ICacheStorage<string> _sessionStorage;
        private TimeSpan _time;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _storage = new MemoryCacheContextStorageProvider();
            _sessionStorage = new MemoryCacheStorage(_storage);
            _time = TimeSpan.FromMinutes(1);
        }

        [Test]
        public void Add_NewObjects_InsertIntoStorageSuccess()
        {
            _sessionStorage.Add("stringTest", "XXXX", _time);
            _sessionStorage.Add("classTest", new TestClass(), _time);

            Assert.AreEqual(2, _storage.Context.Count());
        }

        [Test]
        public void Add_InsertInvalidKeyNotSafe_ThrowsStorageException()
        {
            Assert.Throws<StorageException>(() => _sessionStorage.Add(null, "", _time));
        }

        [Test]
        public void Add_InsertInvalidKeySafe_ThrowsStorageException()
        {
            _sessionStorage.Add(null, "", _time, true);
        }

        [Test]
        public void Get_ByKeyObjectExist_ReturnInstance()
        {
            var res = _sessionStorage.Get<TestClass>("classTest");

            Assert.AreSame(typeof(TestClass), res.GetType());
        }

        [Test]
        public void Get_ByKeyObjectNotExistNotSafe_ThrowStorageException()
        {
            Assert.Throws<StorageException>(() => _sessionStorage.Get<TestClass>("classTest2"));
        }

        [Test]
        public void Get_WhenExpireTime_ThrowStorageException()
        {
            _sessionStorage.Add("classTestTime", new TestClass(), TimeSpan.FromSeconds(2));
            Thread.Sleep(3000);
            Assert.Throws<StorageException>(() => _sessionStorage.Get<TestClass>("classTestTime"));
        }

        [Test]
        public void Get_ByKeyObjectNotExistSafe_ReturnNull()
        {
            var res = _sessionStorage.Get<TestClass>("classTest2", safe: true);

            Assert.IsNull(res);
        }

        [Test]
        public void Remove_ExistItem_RemoveItemFromStorage()
        {
            _sessionStorage.Remove("stringTest");

            Assert.Throws<StorageException>(() => _sessionStorage.Get<string>("stringTest"));
        }
    }
}
