using System;
using com.paralib.Logging;
using System.Runtime.CompilerServices;

namespace com.paralib
{
    public interface ILog
    {
        void Debug(object message = null, Exception exception = null, [CallerMemberName] string methodName = null, [CallerFilePath] string fileName = null, [CallerLineNumber] int? lineNumber = null);
        void Info(object message = null, Exception exception = null, [CallerMemberName] string methodName = null, [CallerFilePath] string fileName = null, [CallerLineNumber] int? lineNumber = null);
        void Warn(object message = null, Exception exception = null, [CallerMemberName] string methodName = null, [CallerFilePath] string fileName = null, [CallerLineNumber] int? lineNumber = null);
        void Error(object message = null, Exception exception = null, [CallerMemberName] string methodName = null, [CallerFilePath] string fileName = null, [CallerLineNumber] int? lineNumber = null);
        void Fatal(object message = null, Exception exception = null, [CallerMemberName] string methodName = null, [CallerFilePath] string fileName = null, [CallerLineNumber] int? lineNumber = null);
        void Log(LogLevels level, object message = null, Exception exception = null, [CallerMemberName] string methodName = null, [CallerFilePath] string fileName = null, [CallerLineNumber] int? lineNumber = null);

    }
}
