using System;
using System.Collections.Generic;
using com.paralib.Logging;

namespace com.paralib.Configuration
{
    public class Settings
    {

        public string Connection { get; set; }

        public class LoggingSettings
        {
            public bool Enabled { get; set; } 
            public bool Debug { get; set; }
            public LogLevels Level { get; set; }
            public List<Log> Logs { get; } = new List<Log>();
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

                //only use the setting (which defaults to true) if defined in file...
                if (paralibSection.Logging.ElementInformation.IsPresent)
                {
                    settings.Logging.Enabled = paralibSection.Logging.Enabled;
                }

                settings.Logging.Debug = paralibSection.Logging.Debug;
                settings.Logging.Level = paralibSection.Logging.Level;

                foreach (LogElement element in paralibSection.Logging.Logs)
                {
                    //we do this to make sure non-applicable attributes are ignored (we could throw errors if we wanted)
                    switch (element.LogType)
                    {
                        case LogTypes.Console:
                            settings.Logging.Logs.Add(new Log(element.Name, element.LogType, element.Enabled) { Pattern = Nullify(element.Pattern), Capture = Nullify(element.Capture) });
                            break;
                        case LogTypes.File:
                            settings.Logging.Logs.Add(new Log(element.Name, element.LogType, element.Enabled) { Pattern = Nullify(element.Pattern), Capture = Nullify(element.Capture), Path= Nullify(element.Path)});
                            break;
                        case LogTypes.Database:
                            settings.Logging.Logs.Add(new Log(element.Name, element.LogType, element.Enabled) { Pattern = Nullify(element.Pattern), Capture = Nullify(element.Capture), Connection= Nullify(element.Connection), ConnectionType= Nullify(element.ConnectionType), Table = Nullify(element.Table), Fields = Nullify(element.Fields) });
                            break;
                        default:
                            throw new ParalibException($"Can't Create LogType {element.LogType}");
                    }

                }


                //DAL
                settings.Dal.Connection = Nullify(paralibSection.Dal.Connection);


            }

            return settings;

        }


    }
}
