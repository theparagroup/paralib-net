using System;
using log4net.Appender;
using log4net.Core;

namespace com.paralib.Logging
{
    public class ParaRollingFileAppender : RollingFileAppender
    {
        public static readonly string DefaultPattern = "%-5p %d %5rms %-22.22logger{1} %-18.18property{method} - %m%n";

    }
}