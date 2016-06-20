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
        private static object _log4netSection;
        private static NameValueCollection _connectionStrings;
        private static NameValueCollection _appSettings;

        static ConfigurationManager()
        {
            Load();
        }


        public static bool HasParalib { get; private set; }
        public static bool HasParalibOverride { get; private set; }
        public static bool HasConnectionStringsOverrides { get; private set; }
        public static bool HasAppSettingsOverrides { get; private set; }
        public static bool HasLog4Net { get; private set; }
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

        public static bool HasLog4NetElement(string path)
        {
            Stream s = new FileStream(path, FileMode.Open);
            System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(s, null);
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(xmlReader);
            System.Xml.XmlNodeList configNodeList = doc.GetElementsByTagName("log4net");
            s.Close();
            return (configNodeList.Count > 0);
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
                
                paralibSection.Logging.Logs.Add(new LogElement() { Name = "file-demo", Enabled = false, LogType=LogTypes.File, Capture = "error,fatal,info-warn", Path ="errors.log", Pattern = "%thread [%property{tid}] %level %logger %property{method} %property{file} %property{line} %property{user} %n" });
                paralibSection.Logging.Logs.Add(new LogElement() { Name = "database-demo", Enabled = false, LogType = LogTypes.Database, Connection = "paralib", ConnectionType= "System.Data.SqlClient.SqlConnection", Table="thelog", Pattern="%.25level, %.256logger", Fields="level_name,loggger_name" });
                paralibSection.Logging.Logs.Add(new LogElement() { Name = "database-standard", Enabled = false, LogType = LogTypes.Database});

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
            //load paralib section (DotNet)
            //note this will return null if there is no <section>, but will return a default section if there is no <paralib>
            _paralibSection = (ParalibSection)NET.ConfigurationManager.GetSection("paralib");
            if (_paralibSection != null)
            {
                HasParalib = _paralibSection.ElementInformation.IsPresent;

                if (!HasParalib)
                {
                    _paralibSection = null;
                }

            }

            //load connectionstrings (DotNet)
            _connectionStrings = new NameValueCollection();

            foreach (NET.ConnectionStringSettings connectionStringSettings in NET.ConfigurationManager.ConnectionStrings)
            {
                _connectionStrings.Add(connectionStringSettings.Name, connectionStringSettings.ConnectionString);
            }

            //load appsettings (DotNet)
            _appSettings = NET.ConfigurationManager.AppSettings;

            //load log4net section (DotNet)
            //note: this will return null if there is no <section> or <log4net>
            //and will return an XML element if there is
            _log4netSection = NET.ConfigurationManager.GetSection("log4net");
            HasLog4Net = (_log4netSection != null);

            //now look for overrides in paralib.config
            NET.Configuration cfg = LoadParalibConfig();

            if (cfg.HasFile)
            {
                if (cfg.GetSection("paralib") != null)
                {
                    ParalibSection overriddenParalibSection = (ParalibSection)cfg.GetSection("paralib");

                    if (overriddenParalibSection.ElementInformation.IsPresent)
                    {
                        _paralibSection = overriddenParalibSection;
                        HasParalibOverride = true;
                    }
                }

                if (cfg.GetSection("connectionStrings") != null)
                {
                    NET.ConnectionStringsSection connectionStringsSection = (NET.ConnectionStringsSection)cfg.GetSection("connectionStrings");

                    foreach (NET.ConnectionStringSettings connectionStringSettings in connectionStringsSection.ConnectionStrings)
                    {
                        //ignore stuff from machine.config or elsewhere
                        if (connectionStringSettings.ElementInformation.IsPresent)
                        {
                            HasConnectionStringsOverrides = true;
                            _connectionStrings.Set(connectionStringSettings.Name, connectionStringSettings.ConnectionString);
                        }
                    }

                }

                if (cfg.GetSection("appSettings") != null)
                {
                    //more major bullshit
                    NET.AppSettingsSection appSettingsSection = (NET.AppSettingsSection)cfg.GetSection("appSettings");

                    //where would other appsettings would come from?
                    if (appSettingsSection.ElementInformation.IsPresent)
                    {
                        foreach (string key in appSettingsSection.Settings.AllKeys)
                        {
                            HasAppSettingsOverrides = true;

                            //this adds as well
                            _appSettings.Set(key, appSettingsSection.Settings[key].Value);
                        }
                    }

                }

                /*
                    //this returns null if there is no <section>
                    //but returns a DefaultSection even if there is no <log4net>
                    //and IsPresent doesn't seem to work.
                    object overriddenLog4NetSection = cfg.GetSection("log4net");
                    if (overriddenLog4NetSection != null)
                    {
                        _log4netSection = overriddenLog4NetSection;
                        HasLog4NetOverride = true;
                    }
                */

                //just open it as an XML file and look
                if (HasLog4NetElement(ParalibConfigPath))
                {
                    _log4netSection = cfg.GetSection("log4net");
                    HasLog4NetOverride = true;
                }

            }
        }

        public static ParalibSection ParalibSection
        {
            get
            {
                return _paralibSection;
            }
        }

        public static object Log4NetSection
        {
            get
            {
                return _log4netSection;
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
