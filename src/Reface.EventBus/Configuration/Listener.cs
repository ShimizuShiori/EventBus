using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.EventBus.Configuration
{
    public class Listener : ConfigurationElement
    {
        [ConfigurationProperty("type")]
        public String Type
        {
            get
            {
                return base["type"].ToString();
            }
            set
            {
                base["type"] = value;
            }
        }
    }
}
