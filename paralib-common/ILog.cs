using System;
using com.paralib.Logging;
using System.Runtime.CompilerServices;

namespace com.paralib
{
    public interface ILog
    {
        void Debug(object message = null, Exception exception = null, [CallerMemberName] string methodName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0);
        void Info(object message = null, Exception exception = null, [CallerMemberName] string methodName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0);
        void Warn(object message = null, Exception exception = null, [CallerMemberName] string methodName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0);
        void Error(object message = null, Exception exception = null, [CallerMemberName] string methodName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0);
        void Fatal(object message = null, Exception exception = null, [CallerMemberName] string methodName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0);
        void Log(LogLevels level, object message = null, Exception exception = null, [CallerMemberName] string methodName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0);

    }
}
