using System;
using System.Configuration;
using System.ComponentModel;
using com.paralib.Logging;

namespace com.paralib.Configuration
{
    public class LoggingElement : ConfigurationElement
    {

        [ConfigurationProperty("enabled", DefaultValue = false)]
        public bool Enabled
        {
            get { return (bool)base["enabled"]; }
            set { base["enabled"] = value; }
        }

        [ConfigurationProperty("debug", DefaultValue = false)]
        public bool Debug
        {
            get { return (bool)base["debug"]; }
            set { base["debug"] = value; }
        }

        [ConfigurationProperty("level", DefaultValue = LogLevels.None)]
        [TypeConverter(typeof(CaseInsensitiveEnum<LogLevels>))]
        public LogLevels Level
        {
            get { return (LogLevels) base["level"]; }
            set { base["level"] = value; }
        }


        [ConfigurationProperty("logs")]
        [ConfigurationCollection(typeof(LogsCollection), AddItemName ="log")]
        public LogsCollection Logs
        {
            get { return ((LogsCollection)(base["logs"])); }
            set { base["log"] = value; }
        }

    }
}
