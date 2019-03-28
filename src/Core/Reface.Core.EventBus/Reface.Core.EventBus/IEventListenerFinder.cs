using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.Core.EventBus
{
    public interface IEventListenerFinder
    {
        IEnumerable<IEventListener> CreateAllEventListeners();
    }
}
