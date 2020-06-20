using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;

namespace Reface.EventBus
{
    public class TinyMapperMapper : IMapper
    {
        private static readonly HashSet<string> bindedKey = new HashSet<string>();

        private string GetKey(Type targetType, Type resultType)
        {
            return $"{targetType} -> {resultType}";
        }

        public T Convert<T>(object target) where T : new()
        {
            Type resultType = typeof(T);
            return (T)this.Convert(target, resultType);
        }

        public object Convert(object target, Type resultType)
        {
            string key = GetKey(target.GetType(), resultType);
            lock (bindedKey)
            {
                if (!bindedKey.Contains(key))
                {
                    TinyMapper.Bind(target.GetType(), resultType);
                    bindedKey.Add(key);
                }
            }
            return TinyMapper.Map(target.GetType(), resultType, target);
        }
    }
}
