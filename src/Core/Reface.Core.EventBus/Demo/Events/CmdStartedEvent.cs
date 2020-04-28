using Reface.Core.EventBus;

namespace Demo.Events
{
    public class CmdStartedEvent : Event
    {
        public CmdStartedEvent(object source) : base(source)
        {
        }
    }
}
