using System;
using System.Collections.Generic;
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
                public static bool Debug { get; internal set; }
                public static LogLevels Level { get; internal set; }
                internal static List<Log> InternalLogs= new List<Log>();
                public static Log[] Logs => InternalLogs.ToArray();

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

                //set paralib's default connectionstring name
                Connection = settings.Connection;

                Logging.Enabled = settings.Logging.Enabled;
                Logging.Debug = settings.Logging.Debug;
                Logging.Level = settings.Logging.Level;
                Logging.InternalLogs = settings.Logging.Logs;

                //(re)configure logging (if enabled)
                LoggingConfigurator.Configure();

                //set DAL's default connectionstring name (could be different)
                Dal.Connection = settings.Dal.Connection;

            }

        }
    }

}
