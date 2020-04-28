using Microsoft.Extensions.DependencyInjection;
using Reface.Core.EventBus.EventListenerFinders;
using System;
using System.Reflection;

namespace Reface.Core.EventBus
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection AddEventBus(this ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICache, DefaultCache>();
            serviceCollection.AddScoped<IEventListenerFinder, ServiceProviderEventListenerFinder>();
            serviceCollection.AddScoped<IEventBus, DefaultEventBus>();
            return serviceCollection;
        }

        public static ServiceCollection AddEventListeners(this ServiceCollection serviceCollection, Assembly assembly)
        {
            Type elType = typeof(IEventListener);
            foreach (var type in assembly.GetTypes())
            {
                if (!elType.IsAssignableFrom(type)) continue;
                serviceCollection.AddScoped(elType, type);
            }
            return serviceCollection;
        }
    }
}
