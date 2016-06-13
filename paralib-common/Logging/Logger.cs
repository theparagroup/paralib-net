using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.common.Logging
{
    public class Logger : ILog
    {
        private readonly static Type _callerStackBoundaryDeclaringType = typeof(Logger);
        private log4net.ILog _logger;

        internal Logger(log4net.ILog logger)
        {
            _logger = logger;
        }

        public void Debug(object message = null, Exception exception = null)
        {
            Log(LogLevels.Debug, message, exception);
        }

        public void Info(object message, Exception exception)
        {
            Log(LogLevels.Info, message, exception);
        }

        public void Warn(object message, Exception exception)
        {
            Log(LogLevels.Warn, message, exception);
        }

        public void Error(object message, Exception exception)
        {
            Log(LogLevels.Error, message, exception);
        }

        public void Fatal(object message, Exception exception)
        {
            Log(LogLevels.Fatal, message, exception);
        }
        
        public void Log(LogLevels level, object message, Exception exception)
        {
            _logger.Logger.Log(_callerStackBoundaryDeclaringType, LogManager.GetLog4NetLevel(level), message, exception);
        }




    }
}
