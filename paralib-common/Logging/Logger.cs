using System;
using System.Runtime.CompilerServices;

namespace com.paralib.Logging
{
    /*
        Improved ILog interface and implementation.

        The following patterns cause a stack trace to be created:

            %type %file %line %method %location %class %C %F %L %l %M

        So we add the new (C# 4.5) compiler attributes to the ILog interface:

            [CallerMemberName] -> property{method}
            [CallerFilePath] -> property{file}
            [CallerLineNumber] -> property{line}

        We also have:

            Thread.ManagedThreadId -> property{tid}
            Thread.CurrentPrincipal -> property{user}

        The "tid" property is just there to compare the thread id at the time the log method
        was called vs when log4net actually appends the event.

        If you don't want to use these, just cast your logger to the log4net.ILog interface.

*/

    public partial class Logger : LoggerBase, ILog
    {
        private readonly static Type _callerStackBoundaryDeclaringType = typeof(Logger);

        internal Logger(log4net.ILog logger):base (logger)
        {
        }

        public override void Log(LogLevels level, object message, Exception exception)
        {
            Logger.Log(_callerStackBoundaryDeclaringType, LogManager.GetLog4NetLevel(level), message, exception);
        }

        void ILog.Log(LogLevels level, object message, Exception exception, string methodName, string fileName, int? lineNumber)
        {
            //can ASP.NET switch the request during a pipeline event? will TLS be preserved?
            log4net.ThreadContext.Properties["tid"] = System.Threading.Thread.CurrentThread.ManagedThreadId;
            log4net.ThreadContext.Properties["user"] = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            log4net.ThreadContext.Properties["method"] = methodName;
            log4net.ThreadContext.Properties["file"] = fileName;
            log4net.ThreadContext.Properties["line"] = lineNumber;

            Log(level, message, exception);
        }

        void ILog.Debug(object message, Exception exception, string methodName, string fileName, int? lineNumber)
        {
            ((ILog)this).Log(LogLevels.Debug, message, exception, methodName, fileName, lineNumber);
        }

        void ILog.Info(object message, Exception exception, string methodName, string fileName, int? lineNumber)
        {
            ((ILog)this).Log(LogLevels.Info, message, exception, methodName, fileName, lineNumber);
        }

        void ILog.Warn(object message, Exception exception, string methodName, string fileName, int? lineNumber)
        {
            ((ILog)this).Log(LogLevels.Warn, message, exception, methodName, fileName, lineNumber);
        }

        void ILog.Error(object message, Exception exception, string methodName, string fileName, int? lineNumber)
        {
            ((ILog)this).Log(LogLevels.Error, message, exception, methodName, fileName, lineNumber);
        }

        void ILog.Fatal(object message, Exception exception, string methodName, string fileName, int? lineNumber)
        {
            ((ILog)this).Log(LogLevels.Fatal, message, exception, methodName, fileName, lineNumber);
        }
    }
}
