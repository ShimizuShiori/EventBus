using System;
using System.Collections.Generic;

namespace Reface.EventBus
{
    /// <summary>
    /// provide the listeners that can handler some EventType
    /// </summary>
    public interface IEventListenerTypeFinder
    {
        IEnumerable<Type> FindListenerTypesByEventType(Type eventType);
    }
}
