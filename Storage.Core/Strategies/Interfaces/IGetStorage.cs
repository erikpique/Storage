namespace Storage.Core.Strategies.Interfaces
{
    public interface IGetStorage<TKey>
    {
        TOutput Get<TOutput>(TKey key, bool safe = false);
    }
}
