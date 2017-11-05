namespace Storage.Core.Strategies.Interfaces
{
    public interface ISessionStorage<TKey> : IAddStorage<TKey>, IGetStorage<TKey>, IRemoveStorage<TKey> { }
}
