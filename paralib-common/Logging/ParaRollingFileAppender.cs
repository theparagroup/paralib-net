using System;
using log4net.Appender;
using log4net.Core;

namespace com.paralib.Logging
{
    public class ParaRollingFileAppender : RollingFileAppender
    {
        public static readonly string DefaultPath = "application.log";
        public static readonly string DefaultPattern = "%date,%timestamp,%thread,%property{tid},%level,%logger,%property{method},%property{user},%message%n";

    }
}