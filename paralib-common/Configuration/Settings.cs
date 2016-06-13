using System;
using System.Collections.Generic;
using com.paralib.Logging;

namespace com.paralib.Configuration
{
    public class Settings
    {

        public string Connection { get; set; }

        public class Log
        {
            public string Name { get; set; }
            public bool Enabled { get; set; }
            public string Capture { get; set; }
            public string Connection { get; set; }
            public string Path { get; set; }
            public string ConnectionType { get; set; }
        }

        public class LoggingSettings
        {
            public bool Enabled { get; set; }
            public Dictionary<string, Log> Logs { get; } = new Dictionary<string, Log>();
        }

        public class DalSettings
        {
            public string Connection { get; set; }
        }

        public Settings()
        {
            Logging = new LoggingSettings();
            Dal = new DalSettings();
        }

        public LoggingSettings Logging { get; private set; }
        public DalSettings Dal { get; private set; }

        private static string Nullify(string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }

        internal static Settings Load(ParalibSection paralibSection)
        {

            Settings settings = new Settings();

            if (paralibSection != null)
            {
                //paralib
                settings.Connection = Nullify(paralibSection.Connection);

                //Logging
                settings.Logging.Enabled = paralibSection.Logging.Enabled;

                foreach (LogElement element in paralibSection.Logging.Logs)
                {
                    switch (element.Type)
                    {
                        case LogTypes.File:
                            settings.Logging.Logs.Add(element.Name, new Log() { Name = element.Name, Enabled = element.Enabled, Capture = Nullify(element.Capture), Path= Nullify(element.Path)});
                            break;
                        case LogTypes.Database:
                            settings.Logging.Logs.Add(element.Name, new Log() { Name = element.Name, Enabled = element.Enabled, Capture = Nullify(element.Capture), Connection= Nullify(element.Connection), ConnectionType= Nullify(element.ConnectionType)});
                            break;
                        default:
                            throw new ParalibException($"Unknown LogType {element.Type}");
                    }

                }


                //DAL
                settings.Dal.Connection = Nullify(paralibSection.Dal.Connection);


            }

            return settings;

        }


    }
}
