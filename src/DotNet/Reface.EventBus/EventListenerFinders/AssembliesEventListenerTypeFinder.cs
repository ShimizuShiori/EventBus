using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reface.EventBus.EventListenerFinders
{
    public class AssembliesEventListenerTypeFinder : IEventListenerTypeFinder
    {
        private readonly IEventListenerTypeFinder defaultFinder;
        private readonly static Type TYPE_LISTENER = typeof(IEventListener);


        public AssembliesEventListenerTypeFinder(IEnumerable<Assembly> assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(x => x.GetTypes())
                .Where(x => TYPE_LISTENER.IsAssignableFrom(x));
            this.defaultFinder = new DefaultEventListenerTypeFinder(types);
        }

        public IEnumerable<Type> FindListenerTypesByEventType(Type eventType)
        {
            return this.defaultFinder.FindListenerTypesByEventType(eventType);
        }
    }
}
