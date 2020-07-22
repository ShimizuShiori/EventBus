using Reface.EventBus.Attributes;
using Reface.EventBus.EventListenerFinders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Reface.EventBus
{
    public class DefaultEventBus : IEventBus
    {
        private readonly ICache cache = EventBusConfiguration.Cache;
        private readonly IEventListenerTypeFinder eventListenerFinder;
        private readonly IMapper mapper = EventBusConfiguration.Mapper;

        public DefaultEventBus(IEventListenerTypeFinder eventListenerFinder)
        {
            this.eventListenerFinder = eventListenerFinder;
        }

        public DefaultEventBus() : this(new ConfigurationEventListenerTypeFinder())
        {

        }


        public void Publish(Event @event)
        {
            this.Publish(@event, 0);
        }

        public void Publish(Event @event, int time)
        {

            var allListeners = this.eventListenerFinder.CreateAllEventListeners();
            allListeners = this.SortEventListeners(allListeners);
            Type eventType = @event.GetType();
            string cacheKey = $"ListenerInfosOf${eventType.FullName}";
            Dictionary<Type, ListenerInfo> infoMap = cache.GetOrCreate(cacheKey,
                () => CreateListenerInfos(eventType, allListeners));

            foreach (var listener in allListeners)
            {
                Type listenerType = listener.GetType();
                if (!infoMap.ContainsKey(listenerType))
                {
                    if (time != 0)
                        throw new KeyNotFoundException($"未发现监听器 [{listenerType.FullName}] 的信息，经清除缓存后，依然无法正常发布。");

                    Debug.WriteLine($"未发现监听器 [{listenerType.FullName}] 的信息，可能与缓存有关，清除缓存将重新发布");
                    cache.Clean(cacheKey);
                    this.Publish(@event, time + 1);
                    return;
                }
            }

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



        /// <summary>
        /// Sort all EventListeners b Priority
        /// </summary>
        /// <param name="listeners"></param>
        /// <returns></returns>
        private IEnumerable<IEventListener> SortEventListeners(IEnumerable<IEventListener> listeners)
        {
            return listeners.Select(x =>
            {
                int p = int.MaxValue;
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
                var baseType = listener.GetType().GetInterface(typeof(IEventListener<>).FullName);
                if (baseType == null)
                {
                    result[listenerType] = new ListenerInfo()
                    {
                        ListenerType = listenerType,
                        ListenerEventType = null,
                        CanTrigger = false
                    };
                    continue;
                }

                Type argType = baseType.GetGenericArguments()[0];
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

        /// <summary>
        /// Create Listener MethodInvoker with Emit
        /// </summary>
        /// <param name="listenerType"></param>
        /// <param name="info"></param>
        /// <returns></returns>
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

        public void Publish(IEventDescriptor info)
        {
            var allListeners = this.eventListenerFinder.CreateAllEventListeners();
            allListeners = this.SortEventListeners(allListeners);
            foreach (var listener in allListeners)
            {
                var methodInfos = this.GetExecutableMethods(listener, info);

                foreach (var method in methodInfos)
                {
                    var ps = method.GetParameters();
                    if (ps.Length == 0)
                    {
                        method.Invoke(listener, new object[] { });
                        continue;
                    }

                    if (ps.Length == 1)
                    {
                        object inputObject = this.mapper.Convert(info.EventData, ps[0].ParameterType);
                        method.Invoke(listener, new object[] { inputObject });
                        continue;
                    }

                }
            }
        }

        private IEnumerable<MethodInfo> GetExecutableMethods(IEventListener listener, IEventDescriptor eventInfo)
        {
            string cacheKey = $"{listener.GetType().FullName} @ {eventInfo.EventType}.{eventInfo.EventName}";
            return this.cache.GetOrCreate<IEnumerable<MethodInfo>>(cacheKey, () => GenerateExecutableMethods(listener, eventInfo));
        }

        private IEnumerable<MethodInfo> GenerateExecutableMethods(IEventListener listener, IEventDescriptor eventInfo)
        {
            string eventType = EventTypeAttribute.GetEventType(listener);
            if (eventType != eventInfo.EventType)
                return Enumerable.Empty<MethodInfo>();

            return listener.GetType()
                .GetMethods()
                .Where(method =>
                {
                    var ps = method.GetParameters();
                    return ps.Length == 0 || ps.Length == 1;
                })
                .Where(method =>
                {
                    string eventName = EventNameAttribute.GetEventName(method);
                    return eventName == eventInfo.EventName;
                }).ToList();

        }
    }
}
