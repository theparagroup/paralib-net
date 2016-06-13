using System;
using NET = System.Configuration;
using com.paralib.Logging;

namespace com.paralib.Configuration
{
    public class ConfigurationManager
    {

        /*
            <paralib>

                <overrides>
                    <connectionStrings/>
                </overrides>

                <logging enabled="false|true" debug="false|true" level="OFF|FATAL" logUser="false|true">
                    <loggers>
                        <logger name="logger1" enabled="true|false" type="file" threshold="OFF|FATAL" filename="application.log"/>
                        <logger name="logger2" enabled="true|false" type="database" threshold="OFF|FATAL" connection="<empty>|mvc" connectionType="System.Data.SqlClient.SqlConnection"/>
                    </loggers>
                </logging>

                <dal connection="oovent" />

                <mvc>
                    <diagnostics enabled="false|true">
                        view loaded assemblies, modules, handler, pipeline trace, other stuff etc.
                    </diagnostics>

                    <authentication/>
                    <authorization/>
                </mvc>

            </paralib>
        */


        public static void CreateParalibSection(NET.Configuration cfg)
        {

            /*
                This is simply to provide a "crib" - as opposed to creating an XML Schema
                for our section. We try to use default values but we want to make sure it's 
                fully populated too.
            */

            if (cfg.Sections["paralib"] == null)
            {
                //<paralib>
                var paralibSection = new ParalibSection();

                //<logging>
                paralibSection.Logging = new LoggingElement();
                paralibSection.Logging.Enabled = false;
                paralibSection.Logging.Debug = false;
                paralibSection.Logging.Level = LogLevels.Off;
                paralibSection.Logging.Loggers.Add(new LoggerElement() { Name = "logger1" });
                paralibSection.Logging.Loggers.Add(new LoggerElement() { Name = "logger2" });

                //<dal>
                paralibSection.Dal = new DalElement();
                paralibSection.Dal.Connection = "paralib";

                //add section
                cfg.Sections.Add("paralib", paralibSection);
                paralibSection.SectionInformation.ForceSave = true;

                //add "starter" connectionstring
                CreateConnectionString(cfg, "paralib", "Data Source=.\\SQLEXPRESS;Initial Catalog={database};Integrated Security=True;");

            }




        }


        public static void Save(System.Configuration.Configuration cfg, bool full = false)
        {

            if (full)
            {
                //this is interesting as it will "dump" the entire configuration, which is quite large.
                cfg.Save(NET.ConfigurationSaveMode.Full, true);
            }
            else
            {
                cfg.Save();
            }


        }


        public static void CreateConnectionString(System.Configuration.Configuration cfg, string name, string connectionString)
        {
            NET.ConnectionStringsSection connections = (NET.ConnectionStringsSection)cfg.GetSection("connectionStrings");

            if (connections.ConnectionStrings[name] == null)
            {
                connections.ConnectionStrings.Add(new NET.ConnectionStringSettings(name, connectionString));
            }

        }


        public static void LoadParalibOverrides()
        {
            //ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            //configMap.ExeConfigFilename = @"d:\test\justAConfigFile.config.whateverYouLikeExtension";
            //Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

        }

        public static ParalibSection GetParalibSection()
        {
            return (ParalibSection)NET.ConfigurationManager.GetSection("paralib");
        }

        public static string GetConnectionString(string name)
        {
            NET.ConnectionStringSettings connectionStringSettings = NET.ConfigurationManager.ConnectionStrings[name];

            if (connectionStringSettings != null)
            {
                return connectionStringSettings.ConnectionString;
            }
            else
            {
                return null;
            }

        }
    }


}
