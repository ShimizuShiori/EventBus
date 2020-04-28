using DemoByNuget.Events;
using Microsoft.Extensions.DependencyInjection;
using Reface.Core.EventBus;
using System;

namespace DemoByNuget
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider serviceProvider = new ServiceCollection()
                .AddEventBus()
                .AddEventListeners(typeof(Program).Assembly)
                .BuildServiceProvider();

            IEventBus eventBus = serviceProvider.GetService<IEventBus>();
            
            eventBus.Publish(new TestEvent(1));
        }
    }
}
