using System;

namespace Reface.EventBus
{
    public interface IListenerCreator
    {
        IEventListener Create(Type listenerType);
    }
}
