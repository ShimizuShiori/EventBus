using Demo.Autofac.Events;
using Demo.Autofac.Model;
using Reface.EventBus;
using System;

namespace Demo.Autofac.Listeners
{
    public class OnUser1Created : IEventListener<ModelCreatedEvent<User1>>
    {
        public void Handle(ModelCreatedEvent<User1> @event)
        {
            Console.WriteLine("User1 Created : {0}", @event.Model.Id);
        }
    }
}
