namespace Reface.EventBus
{
    public interface IEventTrigger
    {
        void Tigger(IEventListener listener, Event @event, EventBusConfiguration configuration);
    }
}
