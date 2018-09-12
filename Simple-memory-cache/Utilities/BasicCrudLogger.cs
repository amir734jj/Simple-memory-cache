using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleMemoryCache.Interfaces;
using static SimpleMemoryCache.Utilities.LambdaHelper;

namespace SimpleMemoryCache.Utilities
{
    public class BasicCrudLogger : IBasicCrud
    {
        public Task<Dictionary<string, KeyValuePair<object, TimeSpan>>> GetAll() =>
            Run(() => Console.WriteLine("GetAll invoked"), Task.FromResult(new Dictionary<string, KeyValuePair<object, TimeSpan>>()));

        public Task Save(string key, object instance, TimeSpan timeSpan) =>
            Run(() => Console.WriteLine($"Save invoked with arg: {key}"), Task.CompletedTask);

        public Task Delete(string key) =>
            Run(() => Console.WriteLine($"Delete invoked with arg: {key}"), Task.CompletedTask);
    }
}