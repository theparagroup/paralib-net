using System;
using log4net.Config;
using System.Reflection;
using System.IO;
using System.Text;
using log4net.Repository.Hierarchy;
using log4net;
using log4net.Core;
using log4net.Appender;
using log4net.Filter;

namespace com.paralib.Logging
{
    public static class LoggingConfiguration
    {
        public static void Configure(LoggingModes loggingMode)
        {

            if (loggingMode==LoggingModes.Basic)
            {
                ConfigureFromResourceStream("com.paralib.Logging.database-with-fallback.xml");
            }
            else if (loggingMode == LoggingModes.Mvc)
            {
                Stream log4net = Assembly.GetExecutingAssembly().GetManifestResourceStream("com.paralib.Logging.database-with-fallback-mvc.xml");
                StreamReader sr = new StreamReader(log4net);
                string xml = sr.ReadToEnd();
                sr.Close();
                log4net.Close();

                ConfigureFromXml(xml);

            }

        }

        public static void ConfigureFromResourceStream(string resourceName)
        {
            Stream log4net = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            XmlConfigurator.Configure(log4net);
            log4net.Close();
        }

        public static void ConfigureFromXml(string xml)
        {
            MemoryStream log4net = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            XmlConfigurator.Configure(log4net);
            log4net.Close();
        }

        public static void Configure2()
        {
            //Hierarchy h = (Hierarchy)LogManager.GetRepository();
            //h.Root.Level = Level.All;

            //IAppender ado = CreateAdoNetAppender(appCode, cs);
            //h.Root.AddAppender(ado);
            //if (onlyErrors)
            //{
            //    var filter = new LevelRangeFilter();
            //    filter.LevelMin = Level.Error;
            //    ((AppenderSkeleton)ado).AddFilter(filter);
            //}

            //h.Configured = true;
        }

    }
}
