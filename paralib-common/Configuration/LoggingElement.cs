using System;
using System.Configuration;
using com.paralib.common.Logging;

namespace com.paralib.common.Configuration
{
    public class LoggingElement : ConfigurationElement
    {
        //enabled="false|true" debug="false|true" level="OFF|FATAL" logUser="false|true"

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

        [ConfigurationProperty("level", DefaultValue = LogLevels.Off)]
        public LogLevels Level
        {
            get { return (LogLevels) base["level"]; }
            set { base["level"] = value; }
        }


        [ConfigurationProperty("loggers")]
        [ConfigurationCollection(typeof(LoggersCollection), AddItemName ="logger")]
        public LoggersCollection Loggers
        {
            get { return ((LoggersCollection)(base["loggers"])); }
            set { base["loggers"] = value; }
        }

    }
}
