using Storage.Core.Exceptions;
using Storage.Core.Providers.Interfaces;
using Storage.Core.Strategies.Interfaces;
using System;
using System.Collections.Generic;

namespace Storage.Core.Strategies
{
    public class MemorySessionStorage : ISessionStorage<string>
    {
        private readonly IContextStorageProvider<IDictionary<string, object>> _storage;

        public MemorySessionStorage(IContextStorageProvider<IDictionary<string, object>> storage)
        {
            _storage = storage;
        }

        public void Add<TInput>(string key, TInput input, bool safe = false)
        {
            try
            {
                _storage.Context.Add(key, input);
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
                return (TOutput)_storage.Context[key];
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
            if (!string.IsNullOrEmpty(key))
            {
                _storage.Context.Remove(key);
            }
        }
    }
}
