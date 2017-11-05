using Storage.Core.Providers.Interfaces;
using System;
using System.Runtime.Caching;

namespace Storage.Core.Providers
{
    public class MemoryCacheContextStorageProvider : IContextStorageProvider<MemoryCache>
    {
        private MemoryCache _memoryCache;

        public MemoryCache Context => _memoryCache = _memoryCache ?? new MemoryCache($"MemoryCache_{DateTime.UtcNow.Ticks}");
    }
}
