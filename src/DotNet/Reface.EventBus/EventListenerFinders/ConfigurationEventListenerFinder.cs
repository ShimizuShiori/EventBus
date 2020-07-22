using Reface.EventBus.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Reface.EventBus.EventListenerFinders
{
    public class ConfigurationEventListenerFinder : IEventListenerTypeFinder
    {
        private readonly string sectionName;
        private readonly Func<List<IEventListener>> creator;

        public ConfigurationEventListenerFinder(string sectionName)
        {
            this.sectionName = sectionName;
            DynamicMethod method = new DynamicMethod("GetEventListeners", typeof(List<IEventListener>), new Type[] { }, typeof(ConfigurationEventListenerFinder));
            var il = method.GetILGenerator();
            il.DeclareLocal(typeof(Object));


            il.Emit(OpCodes.Newobj, typeof(List<IEventListener>).GetConstructor(new Type[] { }));
            il.Emit(OpCodes.Stloc_0);

            var section = EventBusSectionFactory.Get(this.sectionName);
            var list = new List<IEventListener>();
            foreach (var listener in section.Listeners)
            {
                Type lType = Type.GetType((listener as Listener).Type);
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Newobj, lType.GetConstructor(new Type[] { }));
                il.Emit(OpCodes.Callvirt, typeof(List<IEventListener>).GetMethod("Add", new Type[] { typeof(IEventListener) }));
            }
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            creator = (Func<List<IEventListener>>)method.CreateDelegate(typeof(Func<List<IEventListener>>));
        }
        public ConfigurationEventListenerFinder() : this("eventBus")
        {
        }

        public IEnumerable<IEventListener> CreateAllEventListeners()
        {
            return creator();
        }
    }
}
