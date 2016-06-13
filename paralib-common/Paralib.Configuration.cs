using System;
using com.paralib.Configuration;
using com.paralib.Logging;

namespace com.paralib
{

    public partial class Paralib
    {
        public static class Configuration
        {

            /*
                Note: we use "sane" defaults here (off, false, none), in case we 
                don't read the <paralib> section from the config file (manual config).
            */

            public static AppSettings AppSettings { get; } = new AppSettings();

            public static ConnectionStrings ConnectionStrings { get; } = new ConnectionStrings();

            public static string Connection { get; private set; }

            public static string ConnectionString
            {
                get
                {
                    return ConnectionStrings[Connection];
                }
            }


            public static class Logging
            {
                public static bool Enabled { get; internal set; }
            }


            public static class Dal
            {
                public static string Connection { get; internal set; }

                public static string ConnectionString
                {
                    get
                    {
                        if (Connection != null)
                        {
                            return ConnectionStrings[Connection];
                        }
                        else
                        {
                            return Configuration.ConnectionString;
                        }
                    }
                }
            }


            internal static void Load(Settings settings)
            {

                Connection = settings.Connection;

                //unconfigure logging
                LogManager.ResetConfiguration();

                //(re)configure logging
                Configuration.Logging.Enabled = settings.Logging.Enabled;

                if (Configuration.Logging.Enabled)
                {
                    //do something about connection timeout or throw an error or something
                    com.paralib.Logging.LoggingConfiguration.Configure(com.paralib.Logging.LoggingModes.Mvc);
                }

                Configuration.Dal.Connection = settings.Dal.Connection;

            }

        }
    }

}
