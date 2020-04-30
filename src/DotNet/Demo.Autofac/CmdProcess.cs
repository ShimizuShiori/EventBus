using Demo.Autofac.Events;
using Demo.Events;
using Reface.EventBus;
using System;

namespace Demo.Autofac
{
    public interface ICmdProcess
    {
        void Start();
    }

    public class CmdProcess : ICmdProcess
    {
        private readonly IEventBus eventBus;

        public CmdProcess(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public void Start()
        {
            eventBus.Publish(new ConsoleStarted(this));
            while (true)
            {
                string cmd = Console.ReadLine();
                eventBus.Publish(new CommandReadEvent(this, cmd));
            }
        }
    }
}
