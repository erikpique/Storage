namespace Storage.Core.Strategies.Interfaces
{
    public interface ICacheStorage<TKey> : IAddStorageExpire<TKey>, IGetStorage<TKey>, IRemoveStorage<TKey> { }
}
