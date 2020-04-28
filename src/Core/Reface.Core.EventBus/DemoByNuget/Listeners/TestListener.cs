using DemoByNuget.Events;
using Reface.Core.EventBus;
using System;

namespace DemoByNuget.Listeners
{
    public class TestListener : IEventListener<TestEvent>
    {
        public void Handle(TestEvent @event)
        {
            Console.WriteLine("Test");
        }
    }
}
