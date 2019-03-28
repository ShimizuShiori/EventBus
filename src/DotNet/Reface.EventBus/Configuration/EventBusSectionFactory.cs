using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.EventBus.Configuration
{
    class EventBusSectionFactory
    {
        public static EventBusSection Get(String sectionName)
        {
            return (EventBusSection)System.Configuration.ConfigurationManager.GetSection(sectionName);
        }
    }
}
