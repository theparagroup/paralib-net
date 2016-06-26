using System;
using com.paralib.Ado;

namespace com.paralib.Logging
{
    public class Log
    {
        public string Name { get;  }
        public LogTypes LogType { get;  }
        public bool Enabled { get; set; }
        public string LoggerType { get; }
        public string Pattern { get; set; }
        public string Capture { get; set; }
        public string Path { get; set; }
        public string Database { get; set; }
        public string Table { get; set; }
        public string Fields { get; set; }

        public Log(string name, LogTypes logType, bool enabled=true)
        {
            Name = name;
            LogType = logType;
            Enabled = enabled;

            switch (LogType)
            {
                case LogTypes.Console:
                    LoggerType = "ParaConsoleAppender";
                    break;
                case LogTypes.File:
                    LoggerType = "ParaRollingFileAppender";
                    break;
                case LogTypes.Database:
                    LoggerType = "ParaAdoNetAppender";
                    break;
                case LogTypes.Log4Net:
                    throw new ParalibException($"Invalid LogType {LogType}");
                default:
                    throw new ParalibException($"Unknown LogType {LogType}");
            }

        }

        internal Log(string name, LogTypes logType, string loggerType, bool enabled)
        {
            Name = name;
            LogType = logType;
            LoggerType = loggerType;
            Enabled = enabled;
        }

    }
}
