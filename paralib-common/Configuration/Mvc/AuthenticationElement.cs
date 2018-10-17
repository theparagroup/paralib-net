using System;
using System.Configuration;
using System.ComponentModel;

namespace com.paralib.Configuration.Mvc
{
    public class AuthenticationElement : ConfigurationElement
    {

        [ConfigurationProperty("enabled",DefaultValue =true)]
        public bool Enabled
        {
            get { return (bool)base["enabled"]; }
            set { base["enabled"] = value; }
        }

        [ConfigurationProperty("loginUrl")]
        public string LoginUrl
        {
            get { return (string)base["loginUrl"]; }
            set { base["loginUrl"] = value; }
        }

        [ConfigurationProperty("unauthorizedUrl")]
        public string UnauthorizedUrl
        {
            get { return (string)base["unauthorizedUrl"]; }
            set { base["unauthorizedUrl"] = value; }
        }

        [ConfigurationProperty("defaultUrl")]
        public string DefaultUrl
        {
            get { return (string)base["defaultUrl"]; }
            set { base["defaultUrl"] = value; }
        }

        [ConfigurationProperty("global", DefaultValue = true)]
        public bool Global
        {
            get { return (bool)base["global"]; }
            set { base["global"] = value; }
        }

        [ConfigurationProperty("timeout", DefaultValue = 60)]
        public int Timeout
        {
            get { return (int)base["timeout"]; }
            set { base["timeout"] = value; }
        }


    }
}
