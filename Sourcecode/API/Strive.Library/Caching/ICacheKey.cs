namespace Strive.Library.Caching
{
    public interface ICacheKey<TItem>
    {
        string CacheKey { get; }
    }
}