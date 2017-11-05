using System;

namespace Storage.Core.Strategies.Interfaces
{
    public interface IAddStorageExpire<TKey>
    {
        void Add<TInput>(TKey key, TInput input, TimeSpan expire, bool safe = false);
    }
}
