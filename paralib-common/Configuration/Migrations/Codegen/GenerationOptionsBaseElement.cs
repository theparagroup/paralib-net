using System;
using System.Configuration;

namespace com.paralib.Configuration.Migrations.Codegen
{

    public abstract class GenerationOptionsBaseElement : ConfigurationElement
    {

        [ConfigurationProperty("enabled", DefaultValue = true)]
        public bool Enabled
        {
            get { return (bool)base["enabled"]; }
            set { base["enabled"] = value; }
        }

        [ConfigurationProperty("path")]
        public string Path
        {
            get { return (string)base["path"]; }
            set { base["path"] = value; }
        }

        [ConfigurationProperty("namespace")]
        public string Namespace
        {
            get { return (string)base["namespace"]; }
            set { base["namespace"] = value; }
        }

        [ConfigurationProperty("implements")]
        public string Implements
        {
            get { return (string)base["implements"]; }
            set { base["implements"] = value; }
        }

        [ConfigurationProperty("ctor")]
        public string Ctor
        {
            get { return (string)base["ctor"]; }
            set { base["ctor"] = value; }
        }


    }

}
