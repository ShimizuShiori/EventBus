using System;
using System.Collections.Generic;
using System.Linq;

namespace Reface.EventBus.EventListenerFinders
{
    public class DefaultEventListenerTypeFinder : IEventListenerTypeFinder
    {
        private readonly Dictionary<Type, ICollection<Type>> genericListenerTypeMap = new Dictionary<Type, ICollection<Type>>();
        private readonly ICollection<Type> notGenericListenerTypes;
        private readonly static string TYPE_NAME_GENERIC_EVENT_LISTENER = typeof(IEventListener<>).FullName;

        public DefaultEventListenerTypeFinder(IEnumerable<Type> allListenerTypes)
        {
            ICollection<Type> noGenericTypes = new List<Type>();

            foreach (var listenerType in allListenerTypes)
            {
                Type eventType;
                if (!TryGetEventType(listenerType, out eventType))
                {
                    noGenericTypes.Add(listenerType);
                    continue;
                }

                ICollection<Type> genericListenerTypes;
                if (!genericListenerTypeMap.TryGetValue(eventType, out genericListenerTypes))
                {
                    genericListenerTypes = new List<Type>();
                    this.genericListenerTypeMap[eventType] = genericListenerTypes;
                }
                this.notGenericListenerTypes.Add(listenerType);

            }
        }

        private bool TryGetEventType(Type listenerType, out Type eventType)
        {
            Type interfaceType = listenerType.GetInterface(TYPE_NAME_GENERIC_EVENT_LISTENER);
            eventType = null;
            if (interfaceType == null)
                return false;

            eventType = interfaceType.GetGenericArguments()[0];
            return true;
        }

        public IEnumerable<Type> FindListenerTypesByEventType(Type eventType)
        {
            ICollection<Type> result;
            if (this.genericListenerTypeMap.TryGetValue(eventType, out result))
                return result;

            return Enumerable.Empty<Type>();
        }
    }
}
