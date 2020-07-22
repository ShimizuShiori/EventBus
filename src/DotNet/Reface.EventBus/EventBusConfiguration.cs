namespace Reface.EventBus
{
    public static class EventBusConfiguration
    {
        public static ICache Cache { get; set; }
        public static IMapper Mapper { get; set; }

        public static IListenerCreator ListenerCreator { get; set; }

        static EventBusConfiguration()
        {
            Cache = new DefaultCache();
            Mapper = new TinyMapperMapper();
            ListenerCreator = new DefaultListenerCreator();
        }
    }
}
