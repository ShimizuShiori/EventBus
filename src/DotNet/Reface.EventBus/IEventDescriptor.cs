namespace Reface.EventBus
{
    public interface IEventDescriptor
    {
        string EventType { get; }
        string EventName { get; }
        object EventData { get; }
    }
}
