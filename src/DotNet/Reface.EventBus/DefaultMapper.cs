using System;

namespace Reface.EventBus
{
    public class DefaultMapper : IMapper
    {
        public T Convert<T>(object target) where T : new()
        {
            T result = new T();
            foreach (var property in result.GetType().GetProperties())
            {
                if (!property.CanWrite) continue;
                var targetProperty = target.GetType().GetProperty(property.Name);
                if (!targetProperty.CanRead) continue;

                if (!property.PropertyType.IsAssignableFrom(targetProperty.PropertyType))
                    continue;

                property.SetValue(result, targetProperty.GetValue(target, null), null);
            }
            return result;
        }

        public object Convert(object target, Type resultType)
        {
            object result = Activator.CreateInstance(resultType);
            foreach (var property in result.GetType().GetProperties())
            {
                if (!property.CanWrite) continue;
                var targetProperty = target.GetType().GetProperty(property.Name);
                if (!targetProperty.CanRead) continue;

                if (!property.PropertyType.IsAssignableFrom(targetProperty.PropertyType))
                    continue;

                property.SetValue(result, targetProperty.GetValue(target, null), null);
            }
            return result;
        }
    }
}
