using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.Core.EventBus
{
    public class DefaultCache : ICache
    {
        private static readonly ConcurrentDictionary<String, Object> values = new ConcurrentDictionary<string, object>();

        public T GetOrCreate<T>(string name, Func<T> factory)
        {
            return (T)values.GetOrAdd(name, s => factory());
        }
    }
}
