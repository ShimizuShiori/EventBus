using Reface.Core.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Events
{
    public class CommandRevicedEvent : Event
    {
        public string Command { get; private set; }

        public CommandRevicedEvent(string command) : base(null)
        {
            this.Command = command;
        }
    }
}
