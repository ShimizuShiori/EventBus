using Reface.EventBus;

namespace Demo.Autofac.Events
{
    public class CommandReadEvent : Event
    {
        public string Command { get; private set; }
        public CommandReadEvent(object source, string cmd) : base(source)
        {
            this.Command = cmd;
        }
    }
}
