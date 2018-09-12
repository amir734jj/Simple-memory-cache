using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SimpleMemoryCache.Extensions;
using SimpleMemoryCache.Interfaces;

namespace SimpleMemoryCache
{
    public class MemoryCache : IMemoryCache, IDisposable
    {
        /// <summary>
        /// Key-Value data-store
        /// </summary>
        private readonly Dictionary<string, object> _dataStore;
        
        /// <summary>
        /// Lifetimes
        /// </summary>
        private readonly Dictionary<string, TimeSpan> _lifeTimes;

        private DateTime _lastRun;
                
        private readonly Timer _cacheRefresher;
        
        private readonly IBasicCrud _basicCrud;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cacheRefreshTimeSpan"></param>
        /// <param name="basicCrud"></param>
        public MemoryCache(TimeSpan cacheRefreshTimeSpan, IBasicCrud basicCrud)
        {
            // Set the basic crud
            _basicCrud = basicCrud;

            var result = _basicCrud.GetAll().Result;

            // Initialize the data-store
            _dataStore = result.ToDictionary(x => x.Key, x => x.Value.Key);

            // Initialize the life times dictionary
            _lifeTimes = result.ToDictionary(x => x.Key, x => x.Value.Value);

            // Set the cache handler last ran
            _lastRun = DateTime.Now;
            
            // Cache refresher task
            _cacheRefresher = new Timer(e => DataStoreRefreshHandler(), null, cacheRefreshTimeSpan, cacheRefreshTimeSpan);
        }
        
        /// <summary>
        /// Returns an object given a key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            var exist = _dataStore.TryGetValue(key, out var value);

            // If key exist return value else return null
            return exist ? value : null;
        }

        /// <summary>
        /// Deletes the key
        /// </summary>
        /// <param name="key"></param>
        public void Delete(string key)
        {
            // Remove the key-value from data-store
            _dataStore.Remove(key);

            // Remove the interval
            _lifeTimes.Remove(key);
        }

        /// <summary>
        /// Sets the key-value in data-store
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        public void Set(string key, object value, TimeSpan timeSpan)
        {
            // Set data-store value
            _dataStore[key] = value;
            
            // Save interval value
            _lifeTimes[key] = timeSpan;

            // Save the key-value
            _basicCrud.Save(key, value, timeSpan);
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Dispose the refresher task as well
        /// </summary>
        public void Dispose()
        {
            _cacheRefresher.Dispose();
        }

        /// <summary>
        /// Handles refreshing the key-values
        /// </summary>
        private void DataStoreRefreshHandler()
        {
            var currentTime = DateTime.Now;

            _lifeTimes.Copy().ForEach(x =>
            {
                if (currentTime - _lastRun >= x.Value)
                {
                    Delete(x.Key);
                }
            });

            _lastRun = currentTime;
        }
    }
}