using Serialization.Core.Interfaces;
using Storage.Core.Exceptions;
using Storage.Core.Providers.Interfaces;
using Storage.Core.Strategies.Interfaces;
using System;
using System.Linq;
using System.Web;

namespace Storage.Core.Strategies
{
    public class HttpCookieStorage : ICacheStorage<string>
    {
        private readonly IContextStorageProvider<HttpContext> _storage;
        private readonly ISerializer _serializer;

        public HttpCookieStorage(IContextStorageProvider<HttpContext> storage, ISerializer serializer)
        {
            _storage = storage;
            _serializer = serializer;
        }

        public void Add<TInput>(string key, TInput input, TimeSpan expire, bool safe = false)
        {
            try
            {
                var value = _serializer.Serialize(input);

                var cookie = new HttpCookie(key, value)
                {
                    Expires = DateTime.Now.Add(expire)
                };

                if (_storage.Context.Response.Cookies.AllKeys.Contains(key))
                {
                    _storage.Context.Response.Cookies.Set(cookie);
                }
                else
                {
                    _storage.Context.Response.Cookies.Add(cookie);
                }
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
                return _serializer.Deserializer<TOutput>(_storage.Context.Response.Cookies.Get(key).Value);
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
                _storage.Context.Response.Cookies.Remove(key);
            }
        }
    }
}
