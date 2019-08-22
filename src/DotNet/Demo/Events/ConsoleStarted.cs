using System;

namespace Demo.Events
{
    /// <summary>
    /// 控制台启动后的事件
    /// </summary>
    public class ConsoleStarted : Reface.EventBus.Event
    {
        public ConsoleStarted(object source) : base(source)
        {
        }
    }
}
