using System;
using System.Configuration;
using com.paralib.Logging;
using System.ComponentModel;

namespace com.paralib.Configuration
{
    public class LogElement : ConfigurationElement
    {

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("enabled")]
        public bool Enabled
        {
            get { return (bool)base["enabled"]; }
            set { base["enabled"] = value; }
        }

        [ConfigurationProperty("type")]
        [TypeConverter(typeof(CaseInsensitiveEnum<LogTypes>))]
        public LogTypes Type
        {
            get { return (LogTypes)base["type"]; }
            set { base["type"] = value; }
        }

        [ConfigurationProperty("capture")]
        public string Capture
        {
            get { return (string)base["capture"]; }
            set { base["capture"] = value; }
        }

        [ConfigurationProperty("path")]
        public string Path
        {
            get { return (string)base["path"]; }
            set { base["path"] = value; }
        }

        [ConfigurationProperty("connection")]
        public string Connection
        {
            get { return (string)base["connection"]; }
            set { base["connection"] = value; }
        }

        [ConfigurationProperty("connectionType")]
        public string ConnectionType
        {
            get { return (string)base["connectionType"]; }
            set { base["connectionType"] = value; }
        }


    }
}
