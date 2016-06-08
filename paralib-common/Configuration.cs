using System;
using log4net.Config;
using System.Reflection;
using System.IO;

namespace com.paralib.common
{
    public class Configuration
    {

        public static void Configure(string connectionStringName)
        {
            ConnectionStringName = connectionStringName;

            Stream log4net = Assembly.GetExecutingAssembly().GetManifestResourceStream("com.paralib.common.Logging.database-with-fallback-mvc.xml");
            XmlConfigurator.Configure(log4net);
            log4net.Close();

        }

        public static string ConnectionStringName { get; private set; }

        public static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            }
        }

    }
}
