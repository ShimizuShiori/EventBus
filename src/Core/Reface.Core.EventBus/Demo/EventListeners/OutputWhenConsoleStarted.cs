using System;
using System.Collections.Generic;
using System.Text;
using Demo.Events;
using Reface.Core.EventBus;

namespace Demo.EventListeners
{
    public class OutputWhenConsoleStarted : IEventListener<ConsoleStartedEvent>
    {
        public void Handle(ConsoleStartedEvent @event)
        {
            Console.WriteLine("Console Started...");
        }
    }
}
