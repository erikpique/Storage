namespace Storage.Core.Strategies.Interfaces
{
    public interface IAddStorage<TKey>
    {
        void Add<TInput>(TKey key, TInput input, bool safe = false);
    }
}
