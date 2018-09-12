using System;

namespace SimpleMemoryCache.Interfaces
{
    public interface IMemoryCacheBuilder
    {
        IMemoryCacheBuilder SetRefreshInterval(TimeSpan refreshInterval);
        
        IMemoryCacheBuilder SetPersistancy(IBasicCrud basicCrud);
        
        IMemoryCache Build();
    }
}