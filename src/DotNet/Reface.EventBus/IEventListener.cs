using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.EventBus
{
    public interface IEventListener
    {
    }

    public interface IEventListener<TEvent> : IEventListener
        where TEvent : Event
    {
        void Handle(TEvent @event);
    }
}
