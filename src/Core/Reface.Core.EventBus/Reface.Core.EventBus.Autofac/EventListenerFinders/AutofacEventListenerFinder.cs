using System;
using System.Collections.Generic;

namespace Reface.Core.EventBus.EventListenerFinders
{
    public class AutofacEventListenerFinder : IEventListenerFinder
    {
        private readonly Lazy<IEnumerable<IEventListener>> eventListeners;

        public AutofacEventListenerFinder(Lazy<IEnumerable<IEventListener>> eventListeners)
        {
            this.eventListeners = eventListeners;
        }

        public IEnumerable<IEventListener> CreateAllEventListeners()
        {
            return this.eventListeners.Value;
        }
    }
}
