using System;
using System.Reflection;

namespace Reface.EventBus.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EventNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public EventNameAttribute(string name)
        {
            Name = name;
        }
        public static string GetEventName(MethodInfo method)
        {
            object[] attrs = method.GetCustomAttributes(typeof(EventNameAttribute), true);
            if (attrs == null || attrs.Length == 0) return method.Name;
            return ((EventNameAttribute)attrs[0]).Name;
        }
    }
}
