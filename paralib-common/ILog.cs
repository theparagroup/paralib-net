using System;
using com.paralib.common.Logging;


namespace com.paralib.common
{
    public interface ILog
    {
        void Debug(object message=null, Exception exception=null);
        void Info(object message = null, Exception exception = null);
        void Warn(object message = null, Exception exception = null);
        void Error(object message = null, Exception exception = null);
        void Fatal(object message = null, Exception exception = null);
        void Log(LogLevels level, object message = null, Exception exception = null);
    }
}
