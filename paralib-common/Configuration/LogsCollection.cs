using System;
using System.Configuration;

namespace com.paralib.Configuration
{
    [ConfigurationCollection(typeof(LogElement))]
    public class LogsCollection : ConfigurationElementCollection
    {

        public void Add(LogElement element)
        {
            BaseAdd(element);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new LogElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LogElement)(element)).Name;
        }


    }
}
