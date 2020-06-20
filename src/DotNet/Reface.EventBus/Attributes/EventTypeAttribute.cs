using System;

namespace Reface.EventBus.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EventTypeAttribute : Attribute
    {
        public string Type { get; private set; }

        public EventTypeAttribute(string type)
        {
            Type = type;
        }

        public static string GetEventType(object target)
        {
            Type type = target.GetType();
            object[] attrs = type.GetCustomAttributes(typeof(EventTypeAttribute), true);
            if (attrs == null || attrs.Length == 0) return "";
            return ((EventTypeAttribute)attrs[0]).Type;
        }
    }
}
