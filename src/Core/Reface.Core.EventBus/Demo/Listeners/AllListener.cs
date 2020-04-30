using Reface.Core.EventBus;
using System;

namespace Demo.Listeners
{
    public class AllListener : IEventListener<Event>
    {
        public void Handle(Event @event)
        {
            Console.WriteLine("Event Triggered : {0}", @event.GetType().Name);
        }
    }
}
