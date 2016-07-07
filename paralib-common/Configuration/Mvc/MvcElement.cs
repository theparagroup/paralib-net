using System;
using System.Configuration;
using System.ComponentModel;

namespace com.paralib.Configuration.Mvc
{
    public class MvcElement : ConfigurationElement
    {
        [ConfigurationProperty("authentication")]
        public AuthenticationElement Authentication
        {
            get { return ((AuthenticationElement)(base["authentication"])); }
            set { base["authentication"] = value; }
        }

    }
}
