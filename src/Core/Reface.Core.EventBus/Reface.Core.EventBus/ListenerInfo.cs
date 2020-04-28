using System;

namespace Reface.Core.EventBus
{
    class ListenerInfo
    {
        public Type ListenerType { get; set; }
        public Type ListenerEventType { get; set; }
        public Boolean CanTrigger { get; set; }
    }
}
