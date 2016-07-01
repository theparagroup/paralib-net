using System;
using System.Collections.Generic;
using com.paralib.Ado;
using com.paralib.Logging;
using com.paralib.Configuration;
using com.paralib.Configuration.Migrations.Codegen;
using com.paralib.SettingsOptions;

namespace com.paralib
{
    public class Settings
    {
        /*

            This class represents all of the initial settings for the paralib.

            It can be created programatically, read from config file, or modified
            in the Paralib.Configure() event.

        */
        

        public class DalSettings
        {
            public class DatabaseSettings
            {
                public Dictionary<string, Database> Databases { get; } = new Dictionary<string, Database>();
                public string Default { get; set; }
                public bool Sync { get; set; }
            }

            public DatabaseSettings Database { get; private set; } = new DatabaseSettings();

        }

        public LoggingOptions Logging { get; private set; } = new LoggingOptions();

        public DalSettings Dal { get; private set; } = new DalSettings();

        public MigrationsOptions Migrations { get; private set; } = new MigrationsOptions();

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

                //codegen
                settings.Migrations.Codegen.Path=Nullify(paralibSection.Migrations.Codegen.Path);
                settings.Migrations.Codegen.Namespace = Nullify(paralibSection.Migrations.Codegen.Namespace);
                settings.Migrations.Codegen.Convention = Nullify(paralibSection.Migrations.Codegen.Convention);

                if (paralibSection.Migrations.Codegen.Skip.Count>0)
                {
                    List<string> skip = new List<string>();
                    foreach (TableElement element in paralibSection.Migrations.Codegen.Skip)
                    {
                        skip.Add(element.Name);
                    }
                    settings.Migrations.Codegen.Skip = skip.ToArray();
                }

                settings.Migrations.Codegen.Model.Enabled = paralibSection.Migrations.Codegen.Model.Enabled;
                settings.Migrations.Codegen.Model.Path = Nullify(paralibSection.Migrations.Codegen.Model.Path);
                settings.Migrations.Codegen.Model.Namespace = Nullify(paralibSection.Migrations.Codegen.Model.Namespace);
                settings.Migrations.Codegen.Model.Replace = paralibSection.Migrations.Codegen.Model.Replace;
                settings.Migrations.Codegen.Model.Implements = Nullify(paralibSection.Migrations.Codegen.Model.Implements);
                settings.Migrations.Codegen.Model.Ctor = Nullify(paralibSection.Migrations.Codegen.Model.Ctor);

                settings.Migrations.Codegen.Logic.Enabled = paralibSection.Migrations.Codegen.Logic.Enabled;
                settings.Migrations.Codegen.Logic.Path = Nullify(paralibSection.Migrations.Codegen.Logic.Path);
                settings.Migrations.Codegen.Logic.Namespace = Nullify(paralibSection.Migrations.Codegen.Logic.Namespace);
                settings.Migrations.Codegen.Logic.Replace = paralibSection.Migrations.Codegen.Logic.Replace;
                settings.Migrations.Codegen.Logic.Implements = Nullify(paralibSection.Migrations.Codegen.Logic.Implements);
                settings.Migrations.Codegen.Logic.Ctor = Nullify(paralibSection.Migrations.Codegen.Logic.Ctor);

                settings.Migrations.Codegen.Metadata.Enabled = paralibSection.Migrations.Codegen.Metadata.Enabled;
                settings.Migrations.Codegen.Metadata.Path = Nullify(paralibSection.Migrations.Codegen.Metadata.Path);
                settings.Migrations.Codegen.Metadata.Namespace = Nullify(paralibSection.Migrations.Codegen.Metadata.Namespace);
                settings.Migrations.Codegen.Metadata.Replace = paralibSection.Migrations.Codegen.Metadata.Replace;
                settings.Migrations.Codegen.Metadata.Implements = Nullify(paralibSection.Migrations.Codegen.Metadata.Implements);
                settings.Migrations.Codegen.Metadata.Ctor = Nullify(paralibSection.Migrations.Codegen.Metadata.Ctor);

                settings.Migrations.Codegen.Ef.Enabled = paralibSection.Migrations.Codegen.Ef.Enabled;
                settings.Migrations.Codegen.Ef.Path = Nullify(paralibSection.Migrations.Codegen.Ef.Path);
                settings.Migrations.Codegen.Ef.Namespace = Nullify(paralibSection.Migrations.Codegen.Ef.Namespace);
                settings.Migrations.Codegen.Ef.Replace = paralibSection.Migrations.Codegen.Ef.Replace;
                settings.Migrations.Codegen.Ef.Implements = Nullify(paralibSection.Migrations.Codegen.Ef.Implements);
                settings.Migrations.Codegen.Ef.Ctor = Nullify(paralibSection.Migrations.Codegen.Ef.Ctor);

                settings.Migrations.Codegen.Nh.Enabled = paralibSection.Migrations.Codegen.Nh.Enabled;
                settings.Migrations.Codegen.Nh.Path = Nullify(paralibSection.Migrations.Codegen.Nh.Path);
                settings.Migrations.Codegen.Nh.Namespace = Nullify(paralibSection.Migrations.Codegen.Nh.Namespace);
                settings.Migrations.Codegen.Nh.Replace = paralibSection.Migrations.Codegen.Nh.Replace;
                settings.Migrations.Codegen.Nh.Implements = Nullify(paralibSection.Migrations.Codegen.Nh.Implements);
                settings.Migrations.Codegen.Nh.Ctor = Nullify(paralibSection.Migrations.Codegen.Nh.Ctor);



            }

            return settings;

        }


    }
}
