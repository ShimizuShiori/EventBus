using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.Core.EventBus
{
    class ListenerInfo
    {
        public Type ListenerEventType { get; set; }
        public Boolean CanTrigger { get; set; }
    }
}
