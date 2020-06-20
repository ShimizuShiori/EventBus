namespace Reface.EventBus
{
    public class DefaultEventBusOptions
    {
        public IEventListenerFinder EventListenerFinder { get; set; }
        public ICache Cache { get; set; }
        public IMapper Mapper { get; set; }
    }
}
