using NUnit.Framework;
using Storage.Core.Providers;
using Storage.Core.Providers.Interfaces;
using Storage.Core.Strategies;
using Storage.Core.Strategies.Interfaces;
using System.Web;

namespace Storage.Core.Test
{
    public class HttpSessionStorageTest
    {
        private IContextStorageProvider<HttpContext> _storage;
        private ISessionStorage<string> _sessionStorage;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _storage = new HttpContextStorageProvider();
            _sessionStorage = new HttpSessionStorage(_storage);
        }

        [Test]
        public void Add_NewObjects_InsertIntoStorageSuccess()
        {
            //TODO
        }
    }
}
