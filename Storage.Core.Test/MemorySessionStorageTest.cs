using NUnit.Framework;
using Storage.Core.Exceptions;
using Storage.Core.Providers;
using Storage.Core.Providers.Interfaces;
using Storage.Core.Strategies;
using Storage.Core.Strategies.Interfaces;
using Storage.Core.Test.Models;
using System.Collections.Generic;

namespace Storage.Core.Test
{
    [TestFixture]
    public class MemorySessionStorageTest
    {
        private IContextStorageProvider<IDictionary<string, object>> _storage;
        private ISessionStorage<string> _sessionStorage;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _storage = new DictionaryContextStorageProvider();
            _sessionStorage = new MemorySessionStorage(_storage);
        }

        [Test]
        public void Add_NewObjects_InsertIntoStorageSuccess()
        {
            _sessionStorage.Add("stringTest", "XXXX");
            _sessionStorage.Add("classTest", new TestClass());

            Assert.AreEqual(2, _storage.Context.Count);
        }

        [Test]
        public void Add_InsertInvalidKeyNotSafe_ThrowsStorageException()
        {
            Assert.Throws<StorageException>(() => _sessionStorage.Add(null, ""));
        }

        [Test]
        public void Add_InsertInvalidKeySafe_ThrowsStorageException()
        {
            _sessionStorage.Add(null, "", true);
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
        public void Get_ByKeyObjectNotExistSafe_ThrowStorageException()
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
