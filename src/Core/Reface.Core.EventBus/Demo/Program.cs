using Demo.Events;
using Microsoft.Extensions.DependencyInjection;
using Reface.Core.EventBus;
using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection
                .AddEventBus()
                .AddEventListeners(typeof(Program).Assembly);

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IEventBus eventBus = serviceProvider.GetService<IEventBus>();

            eventBus.Publish(new CmdStartedEvent(new Program()));

        }
    }
}
