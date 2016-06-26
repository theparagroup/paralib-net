using System;
using System.Reflection;
using com.paralib.Logging;
using System.Collections.Generic;

namespace com.paralib
{
    public partial class Paralib
    {
        public static class Logging
        {
            public static bool Enabled { get; internal set; }
            public static bool Debug { get; internal set; }
            public static LogLevels Level { get; internal set; }
            internal static List<Log> InternalLogs = new List<Log>();
            public static Log[] Logs => InternalLogs.ToArray();

        }

        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(Assembly.GetCallingAssembly(), type);
        }

        public static ILog GetLogger(string name)
        {
            return LogManager.GetLogger(Assembly.GetCallingAssembly(), name);
        }


    }
}
