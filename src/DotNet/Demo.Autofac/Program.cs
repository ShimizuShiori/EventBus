using Autofac;
using Demo.Events;
using Reface.EventBus;
using System;
using System.Linq;

namespace Demo.Autofac
{
    class Program
    {
        static void Main(string[] args)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(x => typeof(IEventListener).IsAssignableFrom(x))
                .AsImplementedInterfaces();
            containerBuilder.RegisterType<AutofacEventListenerFinder>().AsImplementedInterfaces();
            containerBuilder.RegisterType<DefaultEventBus>().AsImplementedInterfaces();
            IContainer container = containerBuilder.Build();
            ConsoleStarted @event = new ConsoleStarted(1);
            IEventBus eventBus = container.Resolve<IEventBus>();
            eventBus.Publish(@event);
            Console.ReadLine();
        }
    }
}
