using Demo.Autofac.Events;
using Demo.Autofac.Model;
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
            User1 user1 = new User1() { Id = 1, Name = "Felix", Password = "12345678" };
            eventBus.Publish(new EventInfo("User", "Created", user1));
            eventBus.Publish(new EventInfo("User", "Created", user1));
            eventBus.Publish(new ModelCreatedEvent<User1>(this, user1));

            eventBus.Publish(new ConsoleStarted(this));
            while (true)
            {
                string cmd = Console.ReadLine();
                eventBus.Publish(new CommandReadEvent(this, cmd));
            }
        }
    }
}
