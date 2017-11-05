using Storage.Core.Exceptions;
using Storage.Core.Providers.Interfaces;
using Storage.Core.Strategies.Interfaces;
using System;
using System.Web;
using System.Web.Caching;

namespace Storage.Core.Strategies
{
    public class HttpCacheStorage : ICacheStorage<string>
    {
        private readonly IContextStorageProvider<HttpContext> _storage;

        public HttpCacheStorage(IContextStorageProvider<HttpContext> storage)
        {
            _storage = storage;
        }

        public void Add<TInput>(string key, TInput input, TimeSpan expire, bool safe = false)
        {
            try
            {
                _storage.Context.Cache.Insert(key, input, null, DateTime.Now.Add(expire), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            catch (Exception exception)
            {
                if (!safe)
                {
                    throw new StorageException($"It was not possible to add the '{key}' key in the store", exception);
                }
            }
        }

        public TOutput Get<TOutput>(string key, bool safe = false)
        {
            try
            {
                return (TOutput)_storage.Context.Cache.Get(key);
            }
            catch (Exception exception)
            {
                if (!safe)
                {
                    throw new StorageException($"Unable to retrieve the object with '{key}' key from the store", exception);
                }

                return default(TOutput);
            }
        }

        public void Remove(string key)
        {
            _storage.Context.Cache.Remove(key);
        }
    }
}
