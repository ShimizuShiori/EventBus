using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.EventBus.Configuration
{
    class EventBusSection : ConfigurationSection
    {
        [ConfigurationProperty("listeners")]
        public ListenerCollection Listeners
        {
            get
            {
                return (ListenerCollection)base["listeners"];
            }
        }
    }
}
