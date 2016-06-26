using System;
using NET = System.Configuration;
using System.IO;
using System.Collections.Specialized;
using com.paralib.Ado;
using com.paralib.Logging;
using System.Reflection;
using System.Xml;

namespace com.paralib.Configuration
{
    public class ConfigurationManager
    {

        /*
            
            ConfigurationManger loads and caches two configuration sections:

                    <paralib/>
                    <log4net/>

            Either (or both) of these sections can be found in either the app/web config file, 
            or an alternate "paralib.config" file.

            If the "paralib.config" is present, then it is additionally scanned for these sections:

                    <appSettings/>
                    <connectionStrings/>

            These sections are "merged" (added to or overridden) with the in-memory representation
            if of the app/web config file.

            Additionally, this class provides several other features:

                    Automatically configuring <log4net> if logging is enabled in <paralib>
                    Creating a "boilerplate" <paralib> section
                    Merging connectionStrings and appSettings
                    Saving *.config files

        */

        private static ParalibSection _paralibSection;
        private static XmlNode _log4netSection;

        static ConfigurationManager()
        {
            Load();
        }

        public static ParalibSection ParalibSection
        {
            get
            {
                return _paralibSection;
            }
        }

        public static XmlNode Log4NetSection
        {
            get
            {
                return _log4netSection;
            }
        }


        public static bool HasParalib { get; private set; }
        public static bool HasParalibOverride { get; private set; }
        public static bool HasLog4Net { get; private set; }
        public static bool HasLog4NetOverride { get; private set; }
        public static bool HasConnectionStringsOverrides { get; private set; }
        public static bool HasAppSettingsOverrides { get; private set; }


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

        public static XmlNode GetLog4NetXmlNode(string path)
        {
            Stream s = new FileStream(path, FileMode.Open);
            XmlReader xmlReader = XmlReader.Create(s, null);
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlReader);
            s.Close();

            XmlNodeList configNodeList = doc.GetElementsByTagName("log4net");

            XmlNode configNode = null;

            if (configNodeList.Count>0)
            {
                configNode = configNodeList[0];
            }

            return configNode;
        }

        public static string GetConnectionString(string name)
        {
            return NET.ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }


        public static string GetAppSetting(string name)
        {
            return NET.ConfigurationManager.AppSettings[name];
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

                //<logging>
                paralibSection.Logging = new LoggingElement();
                paralibSection.Logging.Enabled = false;
                paralibSection.Logging.Debug = false;
                paralibSection.Logging.Level = LogLevels.Off;
                
                //<logs>
                paralibSection.Logging.Logs.Add(new LogElement() { Name = "file-demo", Enabled = false, LogType=LogTypes.File, Capture = "error,fatal,info-warn", Path ="errors.log", Pattern = "%thread [%property{tid}] %level %logger %property{method} %property{file} %property{line} %property{user} %n" });
                paralibSection.Logging.Logs.Add(new LogElement() { Name = "database-demo", Enabled = false, LogType = LogTypes.Database, Database= "paralib", Table="thelog", Pattern="%.25level, %.256logger{1}", Fields="level_name,loggger_name" });
                paralibSection.Logging.Logs.Add(new LogElement() { Name = "database-standard", Enabled = false, LogType = LogTypes.Database});

                //<dal>
                paralibSection.Dal.Databases.Default = "paralib";
                paralibSection.Dal.Databases.Sync = false;
                paralibSection.Dal.Databases.Add(new DatabaseElement() { Name = "paralib", DatabaseType = DatabaseTypes.SqlServer, Server = ".\\SQLEXPRESS", Store = "store", Integrated = true });
                paralibSection.Dal.Databases.Add(new DatabaseElement() { Name = "foo", DatabaseType = DatabaseTypes.MySql, Server = "127.0.0.1", Port = 99, UserName = "foo", Password = "bar" });

                //add section
                cfg.Sections.Add("paralib", paralibSection);
                paralibSection.SectionInformation.ForceSave = true;

            }

        }

        public static void CreateConnectionString(NET.Configuration cfg, string name, string connectionString, string providerName=null)
        {
            NET.ConnectionStringsSection connections = (NET.ConnectionStringsSection)cfg.GetSection("connectionStrings");

            if (connections.ConnectionStrings[name] == null)
            {
                connections.ConnectionStrings.Add(new NET.ConnectionStringSettings(name, connectionString, providerName));
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

        private static FieldInfo _fiElementReadOnly = typeof(NET.ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
        public static void SetElementReadOnly(NET.ConfigurationElement element, bool value)
        {
            _fiElementReadOnly.SetValue(element, value);
        }

        private static FieldInfo _fiElementCollectionReadOnly= typeof(NET.ConfigurationElementCollection).GetField("bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
        public static void SetElementCollectionReadOnly(NET.ConfigurationElementCollection element, bool value)
        {
            _fiElementCollectionReadOnly.SetValue(element, value);
        }

        public static bool SyncConnectionStringSettings(Database database)
        {
            return SyncConnectionStringSettings(new NET.ConnectionStringSettings(database.Name, database.GetConnectionString(true), database.ProviderName));
        }

        public static bool SyncConnectionStringSettings(NET.ConnectionStringSettings connectionStringSettings)
        {
            NET.ConnectionStringSettings css = NET.ConfigurationManager.ConnectionStrings[connectionStringSettings.Name];

            if (css!=null)
            {
                SetElementReadOnly(NET.ConfigurationManager.ConnectionStrings[connectionStringSettings.Name], false);
                NET.ConfigurationManager.ConnectionStrings[connectionStringSettings.Name].ConnectionString = connectionStringSettings.ConnectionString;
                NET.ConfigurationManager.ConnectionStrings[connectionStringSettings.Name].ProviderName = connectionStringSettings.ProviderName;
                SetElementReadOnly(NET.ConfigurationManager.ConnectionStrings[connectionStringSettings.Name], true);
                return false;
            }
            else
            {
                SetElementCollectionReadOnly(NET.ConfigurationManager.ConnectionStrings, false);
                NET.ConfigurationManager.ConnectionStrings.Add(new NET.ConnectionStringSettings(connectionStringSettings.Name, connectionStringSettings.ConnectionString, connectionStringSettings.ProviderName));
                SetElementCollectionReadOnly(NET.ConfigurationManager.ConnectionStrings, true);
                return true;
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

            //load log4net section (DotNet)
            //note: this will return null if there is no <section> or <log4net>
            //and will return a ConfigXMLElement (:XmlElement:XmlLinkedNode:XmlNode) if there is:
            _log4netSection = NET.ConfigurationManager.GetSection("log4net") as XmlNode;
            HasLog4Net = (_log4netSection != null);

            //now look for overrides in paralib.config
            NET.Configuration cfg = LoadParalibConfig();

            if (cfg.HasFile)
            {
                //use the entire <paralib> from paralib.config if it exists
                if (cfg.GetSection("paralib") != null)
                {
                    ParalibSection overriddenParalibSection = (ParalibSection)cfg.GetSection("paralib");

                    if (overriddenParalibSection.ElementInformation.IsPresent)
                    {
                        _paralibSection = overriddenParalibSection;
                        HasParalibOverride = true;
                    }
                }

                /* use the entire <log4net> from paralib.config if it exists
                
                    this returns null if there is no <section>
                    but returns a DefaultSection even if there is no <log4net>
                    and IsPresent doesn't seem to work:

                        object overriddenLog4NetSection = cfg.GetSection("log4net");
                        if (overriddenLog4NetSection != null)
                        {
                            _log4netSection = overriddenLog4NetSection;
                            HasLog4NetOverride = true;
                        }
                */

                //so just open it as an XML file and look
                XmlNode log4netSection = GetLog4NetXmlNode(ParalibConfigPath);

                if (log4netSection!=null)
                {
                    _log4netSection = log4netSection;
                    HasLog4Net = true;
                    HasLog4NetOverride = true;
                }


                //add or override in-memory <connectionStrings> from paralib.config <connectionStrings>
                if (cfg.GetSection("connectionStrings") != null)
                {
                    NET.ConnectionStringsSection connectionStringsSection = (NET.ConnectionStringsSection)cfg.GetSection("connectionStrings");

                    foreach (NET.ConnectionStringSettings connectionStringSettings in connectionStringsSection.ConnectionStrings)
                    {
                        //ignore stuff from machine.config or elsewhere
                        if (connectionStringSettings.ElementInformation.IsPresent)
                        {
                            HasConnectionStringsOverrides = true;
                            SyncConnectionStringSettings(connectionStringSettings);
                        }
                    }

                }


                //if (cfg.GetSection("connectionStrings") != null)
                //{
                //    NET.ConnectionStringsSection connectionStringsSection = (NET.ConnectionStringsSection)cfg.GetSection("connectionStrings");

                //    foreach (NET.ConnectionStringSettings connectionStringSettings in connectionStringsSection.ConnectionStrings)
                //    {
                //        //ignore stuff from machine.config or elsewhere
                //        if (connectionStringSettings.ElementInformation.IsPresent)
                //        {
                //            HasConnectionStringsOverrides = true;
                //            _connectionStrings.Set(connectionStringSettings.Name, connectionStringSettings.ConnectionString);
                //        }
                //    }

                //}


                //add or override app settings from paralib.config
                if (cfg.GetSection("appSettings") != null)
                {
                    //more major bullshit
                    NET.AppSettingsSection appSettingsSection = (NET.AppSettingsSection)cfg.GetSection("appSettings");

                    //this check is probably not needed - where would other appsettings would come from?
                    if (appSettingsSection.ElementInformation.IsPresent)
                    {
                        foreach (string key in appSettingsSection.Settings.AllKeys)
                        {
                            HasAppSettingsOverrides = true;

                            //this adds as well
                            NET.ConfigurationManager.AppSettings.Set(key, appSettingsSection.Settings[key].Value);
                        }
                    }

                }





            }
        }





    }


}
