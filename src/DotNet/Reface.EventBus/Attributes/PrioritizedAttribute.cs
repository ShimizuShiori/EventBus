using System;

namespace Reface.EventBus.Attributes
{
    public class PrioritizedAttribute : Attribute
    {
        public int Priority { get; private set; }

        public PrioritizedAttribute(int priority)
        {
            Priority = priority;
        }

        public static int GetPriority(Type type)
        {
            object[] attrs = type.GetCustomAttributes(typeof(PrioritizedAttribute), false);
            if (attrs == null || attrs.Length == 0) return int.MaxValue;

            return ((PrioritizedAttribute)attrs[0]).Priority;
        }
    }
}
