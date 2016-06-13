using System;
using System.Configuration;

namespace com.paralib.Configuration
{
    [ConfigurationCollection(typeof(LoggerElement))]
    public class LoggersCollection : ConfigurationElementCollection
    {

        public void Add(LoggerElement element)
        {
            BaseAdd(element);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new LoggerElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LoggerElement)(element)).Name;
        }


    }
}
