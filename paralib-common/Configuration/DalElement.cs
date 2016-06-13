using System;
using System.Configuration;

namespace com.paralib.common.Configuration
{
    public class DalElement : ConfigurationElement
    {

        [ConfigurationProperty("connection")]
        public string Connection
        {
            get { return (string)base["connection"]; }
            set { base["connection"] = value; }
        }
    }
}
