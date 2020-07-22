using Reface.EventBus.EventListenerFinders;

namespace Reface.EventBus
{
    public class EventBusConfiguration
    {
        public ICache Cache { get; set; }
        public IMapper Mapper { get; set; }
        public IListenerCreator ListenerCreator { get; set; }
        public IEventTrigger EventTrigger { get; set; }
        public IEventListenerTypeFinder ListenerTypeFinder { get; set; }
        private EventBusConfiguration()
        {
            Cache = new DefaultCache();
            Mapper = new TinyMapperMapper();
            ListenerCreator = new DefaultListenerCreator();
            EventTrigger = new DefaultEventTrigger();
            ListenerTypeFinder = new ConfigurationEventListenerTypeFinder();
        }
    }
}
