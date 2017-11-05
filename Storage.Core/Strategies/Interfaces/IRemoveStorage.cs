namespace Storage.Core.Strategies.Interfaces
{
    public interface IRemoveStorage<TKey>
    {
        void Remove(TKey key);
    }
}
