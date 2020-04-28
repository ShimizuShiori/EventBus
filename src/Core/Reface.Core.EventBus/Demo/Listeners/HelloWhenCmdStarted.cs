using Demo.Events;
using Reface.Core.EventBus;
using System;

namespace Demo.Listeners
{
    public class HelloWhenCmdStarted : IEventListener<CmdStartedEvent>
    {
        public void Handle(CmdStartedEvent @event)
        {
            Console.WriteLine("Hello");
        }
    }
}
