using Demo.Events;
using Reface.EventBus;
using System;

namespace Demo.Listeners
{
    public class OnConsoleStarted02 : IEventListener<ConsoleStarted>, IPrioritized
    {
        public int Priority => 2;

        public void Handle(ConsoleStarted @event)
        {
            Console.WriteLine("Console Started 02");
        }
    }
}
