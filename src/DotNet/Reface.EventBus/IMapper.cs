using System;

namespace Reface.EventBus
{
    public interface IMapper
    {
        T Convert<T>(object target) where T : new();

        object Convert(object target, Type resultType);
    }
}
