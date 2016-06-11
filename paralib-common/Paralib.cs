using System;
using com.paralib.common.Configuration;

namespace com.paralib.common
{
    public static class Paralib
    {
        private static readonly object _lock = new object();
        private static bool _initialized;

        public static void Initialize()
        {
            Initialize(Settings.Load(ConfigurationManager.GetParalibSection()));
        }

        public static void Initialize(Action<Settings> settings)
        {
            Settings s = new Settings();
            settings(s);
            Initialize(s);
        }

        public static void Initialize(Settings settings)
        {

            lock (_lock)
            {
                if (_initialized)
                {
                    return;
                }

                Configuration.Dal.Connection = settings.Dal.Connection;
                Configuration.Dal.ConnectionString = settings.Dal.ConnectionString;



                //if logging
                Logging.LoggingConfiguration.Configure(Logging.LoggingModes.Mvc);



                _initialized = true;

            }


        }


        public static class Configuration
        {

            public static class Dal
            {
                public static string Connection { get; internal set; }
                public static string ConnectionString { get; internal set; }
            }
        }





    }
}
