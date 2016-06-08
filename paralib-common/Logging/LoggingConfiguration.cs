using System;
using log4net.Config;
using System.Reflection;
using System.IO;

namespace com.paralib.common.Logging
{
    public static class LoggingConfiguration
    {
        public static void Configure(LoggingModes loggingMode)
        {

            if (loggingMode==LoggingModes.Basic)
            {
                Stream log4net = Assembly.GetExecutingAssembly().GetManifestResourceStream("com.paralib.common.Logging.database-with-fallback.xml");
                XmlConfigurator.Configure(log4net);
                log4net.Close();
            }
            else if (loggingMode == LoggingModes.Mvc)
            {
                Stream log4net = Assembly.GetExecutingAssembly().GetManifestResourceStream("com.paralib.common.Logging.database-with-fallback-mvc.xml");
                XmlConfigurator.Configure(log4net);
                log4net.Close();
            }

        }
    }
}
