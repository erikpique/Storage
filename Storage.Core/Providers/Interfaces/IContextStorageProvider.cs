namespace Storage.Core.Providers.Interfaces
{
    public interface IContextStorageProvider<TContext>
    {
        TContext Context { get; }
    }
}
