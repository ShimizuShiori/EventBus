using System;

namespace Reface.EventBus
{
    class ListenerInfo
    {
        public Type ListenerType { get; set; }
        public Type ListenerEventType { get; set; }
        public Boolean CanTrigger { get; set; }
        public int Priority { get; set; }
    }
}
