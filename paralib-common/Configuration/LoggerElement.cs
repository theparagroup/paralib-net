using System;
using System.Configuration;

namespace com.paralib.common.Configuration
{
    public class LoggerElement : ConfigurationElement
    {

        [ConfigurationProperty("name", IsRequired = true)]
        public String Name
        {
            get { return (String)base["name"]; }
            set { base["name"] = value; }
        }

    }
}
