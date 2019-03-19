using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.EventBus
{
    /// <summary>
    /// 事件
    /// </summary>
    public class Event
    {
        /// <summary>
        /// 事件源（事件发起的实例）
        /// </summary>
        public object Source { get; private set; }

        public Event(object source)
        {
            Source = source;
        }
    }
}
