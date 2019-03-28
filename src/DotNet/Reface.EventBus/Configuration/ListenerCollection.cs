using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.EventBus.Configuration
{
    [ConfigurationCollection(typeof(Listener))]
    public class ListenerCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Listener();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as Listener).Type;
        }

        new Listener this[string name]
        {
            get
            {
                return (Listener)base.BaseGet(name);
            }
        }

    }
}
