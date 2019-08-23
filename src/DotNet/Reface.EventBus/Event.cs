using System;
using System.Collections.Generic;

namespace Reface.EventBus
{
    /// <summary>
    /// Event's base class
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Event sender
        /// </summary>
        public object Source { get; private set; }

        /// <summary>
        /// Context
        /// </summary>
        public Dictionary<string, object> Context { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">event sender</param>
        public Event(object source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            Source = source;
            this.Context = new Dictionary<string, object>();
        }
    }
}
