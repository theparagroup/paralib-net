using System;
using System.Reflection;
using com.paralib.Logging;

namespace com.paralib
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
