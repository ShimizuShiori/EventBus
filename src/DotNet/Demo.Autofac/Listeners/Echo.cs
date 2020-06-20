using Demo.Autofac.Events;
using Reface.EventBus;
using System;

namespace Demo.Autofac.Listeners
{
    public class Echo : IEventListener<CommandReadEvent>
    {
        public void Handle(CommandReadEvent @event)
        {
            Console.WriteLine(@event.Command);
        }
    }
}
