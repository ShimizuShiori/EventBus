using System.Collections.Generic;

namespace Reface.EventBus
{
    /// <summary>
    /// provide all EventListeners to DefaultEventBus
    /// </summary>
    public interface IEventListenerFinder
    {
        IEnumerable<IEventListener> CreateAllEventListeners();
    }
}
