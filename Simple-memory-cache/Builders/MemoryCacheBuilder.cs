using System;
using SimpleMemoryCache.Interfaces;
using SimpleMemoryCache.Utilities;
using static SimpleMemoryCache.Utilities.LambdaHelper;

namespace SimpleMemoryCache.Builders
{
    public class MemoryCacheBuilder : IMemoryCacheBuilder
    {
        private TimeSpan _refreshInterval = TimeSpan.FromSeconds(5);

        private IBasicCrud _basicCrud = new BasicCrudLogger();
        
        /// <summary>
        /// Static primary constructor
        /// </summary>
        /// <returns></returns>
        public static MemoryCacheBuilder New() => new MemoryCacheBuilder();
        
        /// <summary>
        /// Sets the refresh interval
        /// </summary>
        /// <param name="refreshInterval"></param>
        /// <returns></returns>
        public IMemoryCacheBuilder SetRefreshInterval(TimeSpan refreshInterval) =>
            Run(() => _refreshInterval = refreshInterval, this);

        /// <summary>
        /// Sets the basic-crud
        /// </summary>
        /// <param name="basicCrud"></param>
        /// <returns></returns>
        public IMemoryCacheBuilder SetPersistancy(IBasicCrud basicCrud) =>
            Run(() => _basicCrud = basicCrud, this);

        /// <summary>
        /// Builds the MemoryCache
        /// </summary>
        /// <returns></returns>
        public IMemoryCache Build() => new MemoryCache(_refreshInterval, _basicCrud);
    }
}