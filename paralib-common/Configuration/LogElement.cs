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

        [ConfigurationProperty("enabled", DefaultValue = true)]
        public bool Enabled
        {
            get { return (bool)base["enabled"]; }
            set { base["enabled"] = value; }
        }

        [ConfigurationProperty("type", DefaultValue = LogTypes.None)]
        [TypeConverter(typeof(CaseInsensitiveEnum<LogTypes>))]
        public LogTypes LogType
        {
            get { return (LogTypes)base["type"]; }
            set { base["type"] = value; }
        }

        [ConfigurationProperty("pattern")]
        public string Pattern
        {
            get { return (string)base["pattern"]; }
            set { base["pattern"] = value; }
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

        [ConfigurationProperty("database")]
        public string Database
        {
            get { return (string)base["database"]; }
            set { base["database"] = value; }
        }

        [ConfigurationProperty("table")]
        public string Table
        {
            get { return (string)base["table"]; }
            set { base["table"] = value; }
        }

        [ConfigurationProperty("fields")]
        public string Fields
        {
            get { return (string)base["fields"]; }
            set { base["fields"] = value; }
        }


    }
}
