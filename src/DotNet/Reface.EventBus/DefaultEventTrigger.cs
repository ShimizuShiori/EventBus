using System.Reflection;

namespace Reface.EventBus
{
    public class DefaultEventTrigger : IEventTrigger
    {

        public void Tigger(IEventListener listener, Event @event, EventBusConfiguration configuration)
        {
            MethodInfo method = listener.GetType().GetMethod("Handle");
            method.Invoke(listener, new object[] { @event });
        }
    }
}
