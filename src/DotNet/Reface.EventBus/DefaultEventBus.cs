﻿using Reface.EventBus.EventListenerFinders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace Reface.EventBus
{
    public class DefaultEventBus : IEventBus
    {
        private readonly ICache cache;
        private readonly IEventListenerFinder eventListenerFinder;

        public DefaultEventBus(ICache cache, IEventListenerFinder eventListenerFinder)
        {
            this.cache = cache;
            this.eventListenerFinder = eventListenerFinder;
        }

        public DefaultEventBus(IEventListenerFinder eventListenerFinder) : this(new DefaultCache(), eventListenerFinder)
        {
        }

        /// <summary>
        /// 默认使用配置文件中的 eventBus section 来加载事件监听器
        /// </summary>
        public DefaultEventBus() : this(new ConfigurationEventListenerFinder())
        {

        }

        public void Publish(Event @event)
        {
            var allListeners = this.eventListenerFinder.CreateAllEventListeners();
            allListeners = this.SortEventListeners(allListeners);
            Type eventType = @event.GetType();
            Dictionary<Type, ListenerInfo> infoMap = cache.GetOrCreate($"ListenerInfosOf${eventType.FullName}",
                () => CreateListenerInfos(eventType, allListeners));

            foreach (var listener in allListeners)
            {
                Type listenerType = listener.GetType();
                var info = infoMap[listenerType];
                if (info.CanTrigger)
                {
                    Action<IEventListener, Object> action = cache.GetOrCreate<Action<IEventListener, Object>>($"TriggerActionOf{listenerType.FullName}",
                        () => CreateListenerTrigger(listenerType, info));
                    action(listener, @event);
                }
            }
        }

        private IEnumerable<IEventListener> SortEventListeners(IEnumerable<IEventListener> listeners)
        {
            return listeners.Select(x =>
            {
                int p = 0;
                if (x is IPrioritized) p = ((IPrioritized)x).Priority;
                return new
                {
                    L = x,
                    P = p
                };
            })
            .OrderBy(x => x.P)
            .Select(x => x.L)
            .ToList();
        }

        private Dictionary<Type, ListenerInfo> CreateListenerInfos(Type eventType, IEnumerable<IEventListener> allListeners)
        {
            Dictionary<Type, ListenerInfo> result = new Dictionary<Type, ListenerInfo>();
            foreach (var listener in allListeners)
            {
                Type listenerType = listener.GetType();
                Type argType = listener.GetType().GetInterface(typeof(IEventListener<>).FullName).GetGenericArguments()[0];
                bool canTrigger = argType.IsAssignableFrom(eventType);
                result[listenerType] = new ListenerInfo()
                {
                    ListenerType = listenerType,
                    CanTrigger = canTrigger,
                    ListenerEventType = argType
                }; ;
            }
            return result;
        }

        private Action<IEventListener, object> CreateListenerTrigger(Type listenerType, ListenerInfo info)
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
        }
    }
}
