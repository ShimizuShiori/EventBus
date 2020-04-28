using Reface.Core.EventBus;

namespace DemoByNuget.Events
{
    public class TestEvent : Event
    {
        public TestEvent(object source) : base(source)
        {
        }
    }
}
