using System;
using System.Collections.Generic;
using System.Text;
using Reface.Core.EventBus;

namespace Demo.Events
{
    public class ConsoleStartedEvent : Event
    {
        public ConsoleStartedEvent() : base(null)
        {
        }
    }
}
