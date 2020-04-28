using System;

namespace Reface.Core.EventBus
{
    public interface ICache
    {
        T GetOrCreate<T>(String name, Func<T> factory);

        void Clean(string name);
    }
}
