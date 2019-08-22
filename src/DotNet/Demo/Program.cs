using Demo.Events;
using Reface.EventBus;
using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // 构造事件总线
            IEventBus eventBus = new DefaultEventBus();

            // 发布消息
            eventBus.Publish(new ConsoleStarted(1));

            Console.ReadLine();
        }
    }
}
