using System;

namespace Reface.EventBus
{
    public class DefaultListenerCreator : IListenerCreator
    {
        public IEventListener Create(Type listenerType)
        {
            return (IEventListener)Activator.CreateInstance(listenerType);
        }
    }
}
