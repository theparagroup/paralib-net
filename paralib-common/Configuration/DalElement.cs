using System;
using System.Configuration;

namespace com.paralib.common.Configuration
{
    public class DalElement : ConfigurationElement
    {

        [ConfigurationProperty("connection", IsRequired = false, DefaultValue ="")]
        public String Connection
        {
            get { return (String)base["connection"]; }
            set { base["connection"] = value; }
        }
    }
}
