using Autofac;
using Reface.EventBus;
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
            containerBuilder.RegisterType<CmdProcess>().AsImplementedInterfaces();
            IContainer container = containerBuilder.Build();

            var process = container.Resolve<ICmdProcess>();
            process.Start();
        }
    }
}
