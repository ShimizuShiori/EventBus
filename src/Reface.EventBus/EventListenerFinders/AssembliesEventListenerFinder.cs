using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Reface.EventBus.EventListenerFinders
{
    public class AssembliesEventListenerFinder : IEventListenerFinder
    {
        private readonly IList<Assembly> assemblies;
        private readonly ICache cache;

        public AssembliesEventListenerFinder(ICache cache, IList<Assembly> assemblies)
        {
            this.cache = cache;
            this.assemblies = assemblies;
        }

        public AssembliesEventListenerFinder(IList<Assembly> assemblies) : this(new DefaultCache(), assemblies)
        {

        }

        public virtual IEnumerable<IEventListener> CreateAllEventListeners()
        {
            Type eventListenerType = typeof(IEventListener);
            return this.assemblies.SelectMany(x => x.GetTypes())
                .Where(x => eventListenerType.IsAssignableFrom(x))
                .Select(x => Activator.CreateInstance(x))
                .OfType<IEventListener>();
        }
    }
}
