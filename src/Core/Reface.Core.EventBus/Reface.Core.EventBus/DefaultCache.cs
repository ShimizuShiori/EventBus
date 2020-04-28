using System;
using System.Collections.Concurrent;

namespace Reface.Core.EventBus
{
    public class DefaultCache : ICache
    {
        private static readonly ConcurrentDictionary<String, Object> values = new ConcurrentDictionary<string, object>();

        public void Clean(string name)
        {
            values.TryRemove(name, out object value);
        }

        public T GetOrCreate<T>(string name, Func<T> factory)
        {
            return (T)values.GetOrAdd(name, s => factory());
        }
    }
}
