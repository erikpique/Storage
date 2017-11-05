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
                _storage.Context.Add(key, input, new DateTimeOffset().Add(expire));
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
                return (TOutput)_storage.Context.Get(key);
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
            _storage.Context.Remove(key);
        }
    }
}
