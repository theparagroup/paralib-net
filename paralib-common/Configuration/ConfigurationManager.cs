using System;
using NET = System.Configuration;
using com.paralib.Logging;
using System.IO;
using System.Collections.Specialized;

namespace com.paralib.Configuration
{
    public class ConfigurationManager
    {

        /*
            <paralib connection="oovent">

                <overrides>
                    <connectionStrings/>
                </overrides>

                <logging enabled="false|true" debug="false|true" level="OFF|FATAL">
                    <logs>
                        <log name="logger1" enabled="true|false" type="file" capture="All|Fatal,Error,Warn,Info,Debug" path="application.log"/>
                        <log name="logger2" enabled="true|false" type="database" capture="All|Fatal,Error,Warn,Info,Debug" connection="<default>|mvc" connectionType="System.Data.SqlClient.SqlConnection"/>
                    </logs>
                </logging>

                <dal connection="<default>|mvc" />

                <mvc>
                    <diagnostics enabled="false|true">
                        view loaded assemblies, modules, handler, pipeline trace, other stuff etc.
                    </diagnostics>

                    <authentication/>
                    <authorization/>
                </mvc>

            </paralib>
        */

        private static ParalibSection _paralibSection;
        private static NameValueCollection _connectionStrings;
        private static NameValueCollection _appSettings;

        static ConfigurationManager()
        {
            Load();
        }

        public static bool HasParalibOverride { get; private set; }
        public static bool HasConnectionStringsOverrides { get; private set; }
        public static bool HasAppSettingsOverrides { get; private set; }
        public static bool HasLog4NetOverride { get; private set; }


        public static string DotNetConfigPath
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            }
        }

        public static string ParalibConfigPath
        {
            get
            {
                //AppDomain.CurrentDomain.SetupInformation.ConfigurationFile -> "P:\pathspec\webapp\web.config"
                //AppDomain.CurrentDomain.SetupInformation.ConfigurationFile -> "P:\pathspec\consoleapp\consoleapp.exe.config"
                //Path.GetDirectoryName("P:\pathspec\webapp\web.config") -> "P:\pathspec\webapp"
                return Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile) + "\\paralib.config";
            }
        }

        public static NET.Configuration LoadDotNetConfig()
        {
            NET.ExeConfigurationFileMap configMap = new NET.ExeConfigurationFileMap();
            configMap.ExeConfigFilename = DotNetConfigPath;
            NET.Configuration cfg = NET.ConfigurationManager.OpenMappedExeConfiguration(configMap, NET.ConfigurationUserLevel.None);
            return cfg;
        }


        public static NET.Configuration LoadParalibConfig()
        {
            NET.ExeConfigurationFileMap configMap = new NET.ExeConfigurationFileMap();
            configMap.ExeConfigFilename = ParalibConfigPath;
            NET.Configuration cfg = NET.ConfigurationManager.OpenMappedExeConfiguration(configMap, NET.ConfigurationUserLevel.None);
            return cfg;
        }



        public static void InitializeConfiguration(NET.Configuration cfg)
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
                paralibSection.Connection = "paralib";

                //<logging>
                paralibSection.Logging = new LoggingElement();
                paralibSection.Logging.Enabled = false;
                paralibSection.Logging.Debug = false;
                paralibSection.Logging.Level = LogLevels.Off;
                paralibSection.Logging.Logs.Add(new LogElement() { Name = "logger1", Enabled=true, LogType=LogTypes.File, Path="errors.log" });
                paralibSection.Logging.Logs.Add(new LogElement() { Name = "logger2", Enabled = false, LogType = LogTypes.Database, Connection = "paralib", ConnectionType= "System.Data.SqlClient.SqlConnection" });

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

        public static void CreateConnectionString(NET.Configuration cfg, string name, string connectionString)
        {
            NET.ConnectionStringsSection connections = (NET.ConnectionStringsSection)cfg.GetSection("connectionStrings");

            if (connections.ConnectionStrings[name] == null)
            {
                connections.ConnectionStrings.Add(new NET.ConnectionStringSettings(name, connectionString));
            }

        }


        public static void SaveConfiguration(NET.Configuration cfg, bool full = false)
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

        private static void Load()
        {
            //load paralib stuff (DotNet)
            _paralibSection = (ParalibSection)NET.ConfigurationManager.GetSection("paralib");

            //load connectionstrings (DotNet)
            _connectionStrings = new NameValueCollection();

            foreach (NET.ConnectionStringSettings connectionStringSettings in NET.ConfigurationManager.ConnectionStrings)
            {
                _connectionStrings.Add(connectionStringSettings.Name, connectionStringSettings.ConnectionString);
            }

            //load appsettings (DotNet)
            _appSettings = NET.ConfigurationManager.AppSettings;

            //now look for overrides in paralib.config
            NET.Configuration cfg = LoadParalibConfig();

            if (cfg.GetSection("paralib") != null)
            {
                HasParalibOverride = true;
                _paralibSection = (ParalibSection)cfg.GetSection("paralib");
            }

            if (cfg.GetSection("connectionStrings") != null)
            {
                NET.ConnectionStringsSection connectionStringsSection = (NET.ConnectionStringsSection)cfg.GetSection("connectionStrings");

                foreach (NET.ConnectionStringSettings connectionStringSettings in connectionStringsSection.ConnectionStrings)
                {
                    HasConnectionStringsOverrides = true;
                    _connectionStrings.Set(connectionStringSettings.Name, connectionStringSettings.ConnectionString);
                }

            }

            if (cfg.GetSection("appSettings") != null)
            {
                //more major bullshit
                NET.AppSettingsSection appSettingsSection = (NET.AppSettingsSection)cfg.GetSection("appSettings");

                foreach (string key in appSettingsSection.Settings.AllKeys)
                {
                    HasAppSettingsOverrides = true;
                    _appSettings.Set(key, appSettingsSection.Settings[key].Value);
                    //_appSettings.Add(key, appSettingsSection.Settings[key].Value);
                }

            }

            if (cfg.GetSection("log4net") != null)
            {
                HasLog4NetOverride = true;
            }
        }

        public static ParalibSection ParalibSection
        {
            get
            {
                return _paralibSection;
            }
        }


        public static string GetConnectionString(string name)
        {
            return _connectionStrings[name];
        }


        public static string GetAppSetting(string name)
        {
            return _appSettings[name];
        }




    }


}
