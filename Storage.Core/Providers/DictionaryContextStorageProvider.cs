using Storage.Core.Providers.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Storage.Core.Providers
{
    public class DictionaryContextStorageProvider : IContextStorageProvider<IDictionary<string, object>>
    {
        private IDictionary<string, object> _memoryCache;

        public IDictionary<string, object> Context => _memoryCache = _memoryCache ?? new ConcurrentDictionary<string, object>();
    }
}
