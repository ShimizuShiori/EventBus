using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.Core.EventBus.EventListenerFinders
{
    public class DefaultEventListenerFinder : IEventListenerFinder
    {
        private readonly Func<IEnumerable<IEventListener>> factory;

        public DefaultEventListenerFinder(Func<IEnumerable<IEventListener>> factory)
        {
            this.factory = factory;
        }

        public IEnumerable<IEventListener> CreateAllEventListeners()
        {
            return this.factory();
        }
    }
}
