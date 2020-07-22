using Reface.EventBus.Configuration;
using System;
using System.Collections.Generic;

namespace Reface.EventBus.EventListenerFinders
{
    public class ConfigurationEventListenerTypeFinder : IEventListenerTypeFinder
    {
        private readonly DefaultEventListenerTypeFinder finder;

        public ConfigurationEventListenerTypeFinder(string sectionName = "eventBus")
        {
            EventBusSection section = EventBusSectionFactory.Get(sectionName);
            ICollection<Type> types = new List<Type>();
            foreach (var item in section.Listeners)
            {
                Listener listener = (Listener)item;
                Type type = Type.GetType(listener.Type);
                types.Add(type);
            }
            this.finder = new DefaultEventListenerTypeFinder(types);
        }

        public IEnumerable<Type> FindListenerTypesByEventType(Type eventType)
        {
            return this.finder.FindListenerTypesByEventType(eventType);
        }
    }
}
