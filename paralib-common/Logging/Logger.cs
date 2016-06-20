using System;
using System.Runtime.CompilerServices;

namespace com.paralib.Logging
{
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

        void ILog.Log(LogLevels level, object message, Exception exception, string methodName, string fileName, int lineNumber)
        {
            //can ASP.NET switch the request during a pipeline event? will TLS be preserved?
            log4net.ThreadContext.Properties["tid"] = System.Threading.Thread.CurrentThread.ManagedThreadId;
            log4net.ThreadContext.Properties["user"] = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            log4net.ThreadContext.Properties["method"] = methodName;
            log4net.ThreadContext.Properties["file"] = fileName;
            log4net.ThreadContext.Properties["line"] = lineNumber;

            Log(level, message, exception);
        }

        void ILog.Debug(object message, Exception exception, string methodName, string fileName, int lineNumber)
        {
            ((ILog)this).Log(LogLevels.Debug, message, exception, methodName, fileName, lineNumber);
        }

        void ILog.Info(object message, Exception exception, string methodName, string fileName, int lineNumber)
        {
            ((ILog)this).Log(LogLevels.Info, message, exception, methodName, fileName, lineNumber);
        }

        void ILog.Warn(object message, Exception exception, string methodName, string fileName, int lineNumber)
        {
            ((ILog)this).Log(LogLevels.Warn, message, exception, methodName, fileName, lineNumber);
        }

        void ILog.Error(object message, Exception exception, string methodName, string fileName, int lineNumber)
        {
            ((ILog)this).Log(LogLevels.Error, message, exception, methodName, fileName, lineNumber);
        }

        void ILog.Fatal(object message, Exception exception, string methodName, string fileName, int lineNumber)
        {
            ((ILog)this).Log(LogLevels.Fatal, message, exception, methodName, fileName, lineNumber);
        }
    }
}
