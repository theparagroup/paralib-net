using com.paralib.Logging;
using System;
using System.Collections.Generic;

namespace com.paralib.SettingsOptions
{
    public class LoggingOptions
    {
        public bool Enabled { get; set; }
        public bool Debug { get; set; }
        public LogLevels Level { get; set; }
        public List<Log> Logs { get; } = new List<Log>();
    }
}
