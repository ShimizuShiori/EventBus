namespace Reface.EventBus
{
    public interface IEventListener
    {
    }

    public interface IEventListener<TEvent> : IEventListener
        where TEvent : Event
    {
        void Handle(TEvent @event);
    }
}
