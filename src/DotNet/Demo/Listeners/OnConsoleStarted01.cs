using Demo.Events;
using Reface.EventBus;
using System;

namespace Demo.Listeners
{
    public class OnConsoleStarted01 : IEventListener<ConsoleStarted>, IPrioritized
    {
        public int Priority => 1;

        public void Handle(ConsoleStarted @event)
        {
            Console.WriteLine("Console Started 01");
        }
    }
}
