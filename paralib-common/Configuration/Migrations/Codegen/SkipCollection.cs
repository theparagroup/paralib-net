using System;
using System.Configuration;

namespace com.paralib.Configuration.Migrations.Codegen
{
    [ConfigurationCollection(typeof(TableElement))]
    public class SkipCollection : ConfigurationElementCollection
    {

        public void Add(TableElement element)
        {
            BaseAdd(element);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new TableElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TableElement)(element)).Name;
        }


    }
}
