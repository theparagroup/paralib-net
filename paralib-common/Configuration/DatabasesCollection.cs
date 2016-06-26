using System;
using System.Configuration;

namespace com.paralib.Configuration
{
    [ConfigurationCollection(typeof(DatabaseElement))]
    public class DatabasesCollection : ConfigurationElementCollection
    {
        [ConfigurationProperty("default")]
        public string Default
        {
            get { return (string)base["default"]; }
            set { base["default"] = value; }
        }

        [ConfigurationProperty("sync", DefaultValue = true)]
        public bool Sync
        {
            get { return (bool)base["sync"]; }
            set { base["sync"] = value; }
        }

        public void Add(DatabaseElement element)
        {
            BaseAdd(element);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DatabaseElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DatabaseElement)(element)).Name;
        }


    }
}
