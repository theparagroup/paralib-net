using System;
using NET = System.Configuration;

namespace com.paralib.common.Configuration
{
    public class Settings
    {

        public class DalSettings
        {
            public string Connection { get; set; }
            public string ConnectionString { get; set; }
        }

        public Settings()
        {
            Dal = new DalSettings();
        }

        public static Settings Load()
        {
            ParalibSection config=(ParalibSection)NET.ConfigurationManager.GetSection("paralib");

            Settings settings = new Settings();

            if (config != null)
            {

                if (config.Dal!= null)
                {
                    //if using config file, connection must point to a valid <connectionStrings> entry
                    settings.Dal.Connection = config.Dal.Connection;

                    if (settings.Dal.Connection != null)
                    {
                        var connectionStringSetting=NET.ConfigurationManager.ConnectionStrings[settings.Dal.Connection];

                        if (connectionStringSetting != null)
                        {
                            settings.Dal.ConnectionString = connectionStringSetting.ConnectionString;
                        }
                        else
                        {
                            throw new ParalibException($"Connection [{settings.Dal.Connection}] not found. Make sure it exists in the <connectionStrings> section.");
                        }

                    }

                }

            }

            return settings;

        }

        public DalSettings Dal { get; private set; }


    }
}
