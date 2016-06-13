using System;
using System.Collections.Generic;

namespace com.paralib.common.Configuration
{
    public class Settings
    {

        public class Logger
        {
            public string Name { get; set; }
        }

        public class LoggingSettings
        {
            public bool Enabled { get; set; }
            public Dictionary<string, Logger> Loggers { get; } = new Dictionary<string, Logger>();
        }

        public class DalSettings
        {
            public string Connection { get; set; }
            public string ConnectionString { get; set; }
        }

        public Settings()
        {
            Logging = new LoggingSettings();
            Dal = new DalSettings();
        }

        public LoggingSettings Logging { get; private set; }
        public DalSettings Dal { get; private set; }


        internal static Settings Load(ParalibSection paralibSection)
        {

            Settings settings = new Settings();

            if (paralibSection != null)
            {

                //Logging
                settings.Logging.Enabled = paralibSection.Logging.Enabled;

                foreach (LoggerElement element in paralibSection.Logging.Loggers)
                {
                    settings.Logging.Loggers.Add(element.Name, new Logger() { Name = element.Name });
                }


                //DAL
                settings.Dal.Connection = paralibSection.Dal.Connection;

                string connectionString = ConfigurationManager.GetConnectionString(settings.Dal.Connection);


            }

            return settings;

        }


    }
}
