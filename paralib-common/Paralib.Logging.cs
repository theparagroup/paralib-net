using System;
using System.Reflection;
using com.paralib.common.Logging;

namespace com.paralib.common
{
    public partial class Paralib
    {

        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(Assembly.GetCallingAssembly(), type);
        }

        public static ILog GetLogger(string name)
        {
            return LogManager.GetLogger(Assembly.GetCallingAssembly(), name);
        }

        public static class Logging
        {
        }

    }
}
