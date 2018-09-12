using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleMemoryCache.Interfaces
{
    public interface IBasicCrud
    {
        Task<Dictionary<string, KeyValuePair<object, TimeSpan>>> GetAll();

        Task Save(string key, object instance, TimeSpan timeSpan);
        
        Task Delete(string key);
    }
}