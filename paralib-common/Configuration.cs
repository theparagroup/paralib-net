using System;
using com.paralib.common.Logging;

namespace com.paralib.common
{
    public class Configuration
    {

        public static void Configure(string connectionStringName, LoggingModes loggingMode)
        {
            ConnectionStringName = connectionStringName;

            LoggingConfiguration.Configure(loggingMode);

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
