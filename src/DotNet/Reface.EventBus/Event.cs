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

        /// <summary>
        /// 事件的上下文对象，可以通过该属性进行事件上下游的信息传递
        /// </summary>
        public Dictionary<string, object> Context { get; private set; }

        public Event(object source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            Source = source;
            this.Context = new Dictionary<string, object>();
        }
    }
}
