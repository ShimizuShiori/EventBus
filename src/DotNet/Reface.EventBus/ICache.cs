using System;

namespace Reface.EventBus
{
    /// <summary>
    /// provide cache to EventBus
    /// </summary>
    public interface ICache
    {
        T GetOrCreate<T>(String name, Func<T> factory);
    }
}
