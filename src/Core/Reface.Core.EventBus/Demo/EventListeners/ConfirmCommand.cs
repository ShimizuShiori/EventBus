using Demo.Events;
using Reface.Core.EventBus;
using System;

namespace Demo.EventListeners
{
    public class ConfirmCommand : IEventListener<CommandRevicedEvent>
    {
        public void Handle(CommandRevicedEvent @event)
        {
            Console.WriteLine($"{@event.Command}!");
        }
    }
}
