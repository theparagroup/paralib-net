using System;
using com.paralib.common.Configuration;

namespace com.paralib.common
{

    public partial class Paralib
    {
        public static class Configuration
        {

            /*
                Note: we use "sane" defaults here (off, false, none), in case we 
                don't read the <paralib> section from the config file (manual config).
            */

            public static ConnectionStrings ConnectionStrings { get; } = new ConnectionStrings();

            public static class Logging
            {
                public static bool Enabled { get; internal set; }
            }


            public static class Dal
            {
                public static string Connection { get; internal set; }
                public static string ConnectionString { get; internal set; }
            }


            internal static void Load(Settings settings)
            {
                //DAL (ugh do this first till we fix)
                Configuration.Dal.Connection = settings.Dal.Connection;
                Configuration.Dal.ConnectionString = ConfigurationManager.GetConnectionString(Configuration.Dal.Connection);


                //unconfigure logging

                Configuration.Logging.Enabled = settings.Logging.Enabled;

                if (Configuration.Logging.Enabled)
                {
                    //do something about connection timeout or throw an error or something
                    com.paralib.common.Logging.LoggingConfiguration.Configure(com.paralib.common.Logging.LoggingModes.Mvc);
                }




                //if (connectionString != null)
                //{
                //    settings.Dal.ConnectionString = connectionString;
                //}
                //else
                //{
                //    throw new ParalibException($"Connection [{settings.Dal.Connection}] not found. Make sure it exists in the <connectionStrings> section.");
                //}

            }

        }
    }

}
