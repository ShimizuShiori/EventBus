namespace Reface.EventBus
{
    public class EventInfo
    {
        public string EventType { get; private set; }
        public string EventName { get; private set; }
        public object EventData { get; private set; }

        public EventInfo(string eventType, string eventName, object eventData)
        {
            EventType = eventType;
            EventName = eventName;
            EventData = eventData;
        }
    }
}
