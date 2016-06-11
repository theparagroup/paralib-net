using System;

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

        public static Settings Load(ParalibSection paralibSection)
        {

            Settings settings = new Settings();

            if (paralibSection != null)
            {

                if (paralibSection.Dal!= null)
                {
                    //if using config file, connection must point to a valid <connectionStrings> entry
                    settings.Dal.Connection = paralibSection.Dal.Connection;

                    if (settings.Dal.Connection != null)
                    {
                        string connectionString=ConfigurationManager.GetConnectionString(settings.Dal.Connection);

                        if (connectionString != null)
                        {
                            settings.Dal.ConnectionString = connectionString;
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
