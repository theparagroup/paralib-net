using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Logging
{
    public class LogManager
    {
        public static ILog GetLogger(Assembly repositoryAssembly, Type type)
        {
            return new Logger(log4net.LogManager.GetLogger(repositoryAssembly, type.FullName));
        }

        public static ILog GetLogger(Type type)
        {
            return new Logger(log4net.LogManager.GetLogger(Assembly.GetCallingAssembly(), type.FullName));
        }

        public static ILog GetLogger(Assembly repositoryAssembly, string name)
        {
            return new Logger(log4net.LogManager.GetLogger(repositoryAssembly, name));
        }

        public static ILog GetLogger(string name)
        {
            return new Logger(log4net.LogManager.GetLogger(Assembly.GetCallingAssembly(), name));
        }

        internal static log4net.Core.Level GetLog4NetLevel(LogLevels level)
        {
            switch (level)
            {
                case LogLevels.All:
                    return log4net.Core.Level.All;
                case LogLevels.None:
                    return log4net.Core.Level.Off;
                case LogLevels.Debug:
                    return log4net.Core.Level.Debug;
                case LogLevels.Info:
                    return log4net.Core.Level.Info;
                case LogLevels.Warn:
                    return log4net.Core.Level.Warn;
                case LogLevels.Error:
                    return log4net.Core.Level.Error;
                case LogLevels.Fatal:
                    return log4net.Core.Level.Fatal;
                case LogLevels.Off:
                    return log4net.Core.Level.Off;
                default:
                    throw new ParalibException($"Unknown or bad logging level [{level}]");
            }
        }

        internal static LogLevels GetLogLevel(log4net.Core.Level level)
        {
            return (LogLevels)level.Value;
        }


    }
}
