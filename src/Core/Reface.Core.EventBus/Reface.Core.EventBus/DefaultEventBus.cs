using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Reface.Core.EventBus
{
    public class DefaultEventBus : IEventBus
    {
        private readonly ICache cache;
        private readonly IEnumerable<IEventListener> allListeners;

        public DefaultEventBus(ICache cache, IEventListenerFinder eventListenerFinder)
        {
            this.cache = cache;
            allListeners = eventListenerFinder.CreateAllEventListeners();
        }

        public DefaultEventBus(IEventListenerFinder eventListenerFinder) : this(new DefaultCache(), eventListenerFinder)
        {

        }
        

        public void Publish(Event @event)
        {
            Type eventType = @event.GetType();
            Dictionary<Type, ListenerInfo> infoMap = cache.GetOrCreate<Dictionary<Type, ListenerInfo>>($"ListenerInfosOf${eventType.FullName}", () =>
            {
                Dictionary<Type, ListenerInfo> result = new Dictionary<Type, ListenerInfo>();
                foreach (var listener in allListeners)
                {
                    Type listenerType = listener.GetType();
                    Type argType = listener.GetType().GetInterface(typeof(IEventListener<>).FullName).GetGenericArguments()[0];
                    bool canTrigger = argType.IsAssignableFrom(eventType);
                    result[listenerType] = new ListenerInfo()
                    {
                        CanTrigger = canTrigger,
                        ListenerEventType = argType
                    };
                }
                return result;
            });
            foreach (var listener in allListeners)
            {
                Type listenerType = listener.GetType();
                var info = infoMap[listenerType];
                if (info.CanTrigger)
                {
                    Action<IEventListener, Object> action = cache.GetOrCreate<Action<IEventListener, Object>>($"TriggerActionOf{listenerType.FullName}", () =>
                    {
                        DynamicMethod method = new DynamicMethod("TriggerMethod", null, new Type[] { typeof(IEventListener), typeof(object) });
                        var il = method.GetILGenerator();
                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Castclass, listenerType);
                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Castclass, info.ListenerEventType);
                        il.Emit(OpCodes.Callvirt, listenerType.GetMethod("Handle", new Type[] { info.ListenerEventType }));
                        il.Emit(OpCodes.Ret);
                        return (Action<IEventListener, Object>)method.CreateDelegate(typeof(Action<IEventListener, Object>));
                    });
                    action(listener, @event);
                }
            }
        }
    }
}
