using System;
using System.Collections.Generic;
using com.paralib.Ado;
using com.paralib.Logging;

namespace com.paralib.Configuration
{
    public class Settings
    {
        /*

            This class represents all of the initial settings for the paralib.

            It can be created programatically, read from config file, or modified
            in the Paralib.Configure() event.

        */

        public class LoggingSettings
        {
            public bool Enabled { get; set; } 
            public bool Debug { get; set; }
            public LogLevels Level { get; set; }
            public List<Log> Logs { get; } = new List<Log>();
        }

        public class DalSettings
        {
            public class DatabaseSettings
            {
                public Dictionary<string, Database> Databases { get; } = new Dictionary<string, Database>();
                public string Default { get; set; }
                public bool Sync { get; set; }
            }

            public DalSettings()
            {
                Database = new DatabaseSettings();
            }

            public DatabaseSettings Database { get; private set; }

        }

        public class MigrationsSettings
        {
            public bool Devmode { get; set; }
            public string Database { get; set; }
            public string Commands { get; set; }
        }

        public Settings()
        {
            Logging = new LoggingSettings();
            Dal = new DalSettings();
            Migrations = new MigrationsSettings();
        }

        public LoggingSettings Logging { get; private set; }

        public DalSettings Dal { get; private set; }

        public MigrationsSettings Migrations { get; private set; }

        private static string Nullify(string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }

        internal static Settings Create(ParalibSection paralibSection)
        {

            Settings settings = new Settings();

            if (paralibSection != null)
            {


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
                            settings.Logging.Logs.Add(new Log(element.Name, element.LogType, element.Enabled) { Pattern = Nullify(element.Pattern), Capture = Nullify(element.Capture), Database=Nullify(element.Database), Table = Nullify(element.Table), Fields = Nullify(element.Fields) });
                            break;
                        default:
                            throw new ParalibException($"Can't Create LogType {element.LogType}");
                    }

                }


                //dal
                settings.Dal.Database.Default = Nullify(paralibSection.Dal.Databases.Default);
                settings.Dal.Database.Sync = paralibSection.Dal.Databases.Sync;

                foreach (DatabaseElement element in paralibSection.Dal.Databases)
                {
                    settings.Dal.Database.Databases.Add(element.Name, new Database(element.Name, element.DatabaseType) { Server = element.Server, Port = element.Port, Store = Nullify(element.Store), Integrated = element.Integrated, UserName = Nullify(element.UserName), Password = Nullify(element.Password), Parameters = Nullify(element.Parameters) });
                }

                //migrations
                settings.Migrations.Devmode = paralibSection.Migrations.Devmode;
                settings.Migrations.Database = Nullify(paralibSection.Migrations.Database);
                settings.Migrations.Commands = Nullify(paralibSection.Migrations.Commands);

            }

            return settings;

        }


    }
}
