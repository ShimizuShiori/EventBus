using Autofac;
using Reface.EventBus;
using System.Collections.Generic;

namespace Demo.Autofac
{
    public class AutofacEventListenerFinder : IEventListenerFinder
    {
        private readonly ILifetimeScope lifetimeScope;

        public AutofacEventListenerFinder(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public IEnumerable<IEventListener> CreateAllEventListeners()
        {
            return this.lifetimeScope.Resolve<IEnumerable<IEventListener>>();
        }
    }
}
