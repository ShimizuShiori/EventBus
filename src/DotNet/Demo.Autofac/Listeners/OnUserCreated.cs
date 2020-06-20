using Demo.Autofac.Model;
using Reface.EventBus;
using Reface.EventBus.Attributes;
using System;

namespace Demo.Autofac.Listeners
{
    [EventType("User")]
    public class OnUserCreated : IEventListener
    {
        [EventName("Created")]
        public void OnCreated1(User2 user)
        {
            Console.WriteLine("用户 : {0}-{1} 被创建", user.Id, user.Name);
        }

        [EventName("Created")]
        public void OnCreated2(User2 user)
        {
            Console.WriteLine("用户 : {0}-{1} 被创建，第二次消息", user.Id, user.Name);
        }
    }
}
