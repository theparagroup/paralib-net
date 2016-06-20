using System;
using log4net.Core;

namespace com.paralib.Logging
{
    public enum LogLevels
    {
        All=int.MinValue,
        Unspecified=0,
        Debug=30000,
        Info=40000,
        Warn=60000,
        Error=70000,
        Fatal=110000,
        Off=int.MaxValue
    }
}
