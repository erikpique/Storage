using Storage.Core.Exceptions;
using Storage.Core.Providers.Interfaces;
using Storage.Core.Strategies.Interfaces;
using System;
using System.Runtime.Caching;

namespace Storage.Core.Strategies
{
    public class MemoryCacheStorage : ICacheStorage<string>
    {
        private readonly IContextStorageProvider<MemoryCache> _storage;

        public MemoryCacheStorage(IContextStorageProvider<MemoryCache> storage)
        {
            _storage = storage;
        }

        public void Add<TInput>(string key, TInput input, TimeSpan expire, bool safe = false)
        {
            try
            {
                _storage.Context.Set(key, input, new DateTimeOffset(DateTime.UtcNow).Add(expire));
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
            var res = (TOutput)_storage.Context.Get(key);

            if (res == null)
            {
                if (!safe)
                {
                    throw new StorageException($"Unable to retrieve the object with '{key}' key from the store");
                }

                return default(TOutput);
            }

            return res;
        }

        public void Remove(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                _storage.Context.Remove(key);
            }
        }
    }
}
