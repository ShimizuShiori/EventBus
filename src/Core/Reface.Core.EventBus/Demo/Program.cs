using System;
using Reface.Core.EventBus;
using Autofac;
using Demo.EventListeners;
using System.Collections;
using System.Collections.Generic;
using Reface.Core.EventBus.EventListenerFinders;
using Demo.Events;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<OutputWhenConsoleStarted>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ConfirmCommand>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AutofacEventListenerFinder>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<DefaultEventBus>().AsImplementedInterfaces().InstancePerLifetimeScope();
            var container = builder.Build();

            IEventBus eb = container.Resolve<IEventBus>();

            eb.Publish(new ConsoleStartedEvent());

            while (true)
            {
                string command = Console.ReadLine();
                eb.Publish(new CommandRevicedEvent(command));
            }
        }
    }
}
