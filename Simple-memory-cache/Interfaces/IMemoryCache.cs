using System;
using System.Threading.Tasks;

namespace SimpleMemoryCache.Interfaces
{
    public interface IMemoryCache
    {
        object Get(string key);

        void Set(string key, object value, TimeSpan lifeTimeSpan);
        
        void Delete(string key);
    }
}