using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Reface.Core.EventBus.EventListenerFinders
{
    public class ServiceProviderEventListenerFinder : IEventListenerFinder
    {
        private readonly IServiceProvider serviceProvider;

        public ServiceProviderEventListenerFinder(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IEnumerable<IEventListener> CreateAllEventListeners()
        {
            return this.serviceProvider.GetServices<IEventListener>();
        }
    }
}
