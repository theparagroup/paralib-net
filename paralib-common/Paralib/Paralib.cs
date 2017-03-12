using System;
using com.paralib.Configuration;
using com.paralib.Logging;
using com.paralib.Ado;
using com.paralib.SettingsOptions;
using com.paralib.ParalibProperties;
using com.paralib.ParalibProperties.Mvc;

namespace com.paralib
{

    public static partial class Paralib
    {
        private static ILog _logger = GetLogger(typeof(Paralib));
        private static readonly object _lock = new object();
        private static bool _initialized;
        private static Settings _settings = new Settings();

        public static long InitializedAt { get; private set; }

        public static MigrationsProperties Migrations { get; } =new MigrationsProperties();
        public static MvcProperties Mvc { get; } = new MvcProperties();

        /* 
        
            All applications call one of these Initialize methods to configure the
            Paralib. The Paralib will work without being initialized, but only with
            default settings, which doesn't do very much.

        */

        public static void Initialize()
        {
            //use a config file (app/web or paralib)
            Initialize(Settings.Create(ConfigurationManager.ParalibSection));
            _logger.Info("initializing with config file");
        }

        public static void Initialize(Action<Settings> settings)
        {
            //use action syntax
            Settings s = new Settings();
            settings(s);
            Initialize(s);

            _logger.Info("initializing with action syntax");
        }

        public static void Initialize(Settings settings)
        {
            //TODO should this call RaiseConfigureEvent?
            lock (_lock)
            {
                if (!_initialized)
                {
                    //initital setup of the paralib
                    _settings = settings;
                    Load();

                    InitializedAt = DateTime.Now.Ticks;
                    _initialized = true;

                    _logger.Info("initializing with explicit settings");
                    _logger.Info("paralib initialized");
                }
            }

        }

        public static Settings Settings
        {
            get
            {
                return _settings;
            }

        }


        /*
            Configure Event:
                This is a static event, so make sure you unsubcribe your instances.
                This event does not follow the standard EventHandler pattern.
                Just for fun, we use explicit add/remove event methods and make the
                event thread-safe.
        */
        private static event ConfigureEventHandler _configureEvent;

        public static event ConfigureEventHandler Configure
        {
            add
            {
                lock (_lock)
                {
                    _configureEvent += value;
                }
            }
            remove
            {
                lock (_lock)
                {
                    _configureEvent -= value;
                }
            }
        }

        public static void RaiseConfigureEvent()
        {
            //allow library consumers to modify configuration programatically

            ConfigureEventHandler handler;

            lock (_lock)
            {
                handler = _configureEvent;
            }

            if (handler != null)
            {
                _logger.Info("configure event raised");

                //send handlers the stored settings object to modify
                handler(_settings);

                //call setup again
                Load();
            }

        }

        /*
                Load()

                When settings are loaded, things happen.

        */
        internal static void Load()
        {

            //dal
            //set paralib's databases dictionary and default
            Dal.Databases = new DatabaseDictionary(_settings.Dal.Database.Databases, _settings.Dal.Database.Default, _settings.Dal.Database.Sync);

            //add paralib databases to in-memory <connectionStrings>
            if (_settings.Dal.Database.Sync)
            {
                foreach (Database database in _settings.Dal.Database.Databases.Values)
                {
                    ConfigurationManager.SyncConnectionStringSettings(database);
                }
            }

            //logging
            Logging.Enabled = _settings.Logging.Enabled;
            Logging.Debug = _settings.Logging.Debug;
            Logging.Level = _settings.Logging.Level;
            Logging.InternalLogs = _settings.Logging.Logs;

            //(re)configure logging (if enabled)
            LoggingConfigurator.Configure();

            //migrations
            Migrations.Devmode = _settings.Migrations.Devmode;
            Migrations.Database = _settings.Migrations.Database;
            Migrations.Commands = _settings.Migrations.Commands;

            Migrations.Codegen.Path = _settings.Migrations.Codegen.Path;
            Migrations.Codegen.Namespace = _settings.Migrations.Codegen.Namespace;
            Migrations.Codegen.Skip = _settings.Migrations.Codegen.Skip;
            Migrations.Codegen.Convention = _settings.Migrations.Codegen.Convention;

            Migrations.Codegen.Model.Enabled = _settings.Migrations.Codegen.Model.Enabled;
            Migrations.Codegen.Model.Path = _settings.Migrations.Codegen.Model.Path;
            Migrations.Codegen.Model.Namespace = _settings.Migrations.Codegen.Model.Namespace;
            Migrations.Codegen.Model.Replace = _settings.Migrations.Codegen.Model.Replace;
            Migrations.Codegen.Model.Implements = _settings.Migrations.Codegen.Model.Implements;
            Migrations.Codegen.Model.Ctor= _settings.Migrations.Codegen.Model.Ctor;

            Migrations.Codegen.Logic.Enabled = _settings.Migrations.Codegen.Logic.Enabled;
            Migrations.Codegen.Logic.Path = _settings.Migrations.Codegen.Logic.Path;
            Migrations.Codegen.Logic.Namespace = _settings.Migrations.Codegen.Logic.Namespace;
            Migrations.Codegen.Logic.Replace = _settings.Migrations.Codegen.Logic.Replace;
            Migrations.Codegen.Logic.Implements = _settings.Migrations.Codegen.Logic.Implements;
            Migrations.Codegen.Logic.Ctor = _settings.Migrations.Codegen.Logic.Ctor;

            Migrations.Codegen.Metadata.Enabled = _settings.Migrations.Codegen.Metadata.Enabled;
            Migrations.Codegen.Metadata.Path = _settings.Migrations.Codegen.Metadata.Path;
            Migrations.Codegen.Metadata.Namespace = _settings.Migrations.Codegen.Metadata.Namespace;
            Migrations.Codegen.Metadata.Replace = _settings.Migrations.Codegen.Metadata.Replace;
            Migrations.Codegen.Metadata.Implements = _settings.Migrations.Codegen.Metadata.Implements;
            Migrations.Codegen.Metadata.Ctor = _settings.Migrations.Codegen.Metadata.Ctor;

            Migrations.Codegen.Ef.Enabled = _settings.Migrations.Codegen.Ef.Enabled;
            Migrations.Codegen.Ef.Path = _settings.Migrations.Codegen.Ef.Path;
            Migrations.Codegen.Ef.Namespace = _settings.Migrations.Codegen.Ef.Namespace;
            Migrations.Codegen.Ef.Replace = _settings.Migrations.Codegen.Ef.Replace;
            Migrations.Codegen.Ef.Implements = _settings.Migrations.Codegen.Ef.Implements;
            Migrations.Codegen.Ef.Ctor = _settings.Migrations.Codegen.Ef.Ctor;

            Migrations.Codegen.Nh.Enabled = _settings.Migrations.Codegen.Nh.Enabled;
            Migrations.Codegen.Nh.Path = _settings.Migrations.Codegen.Nh.Path;
            Migrations.Codegen.Nh.Namespace = _settings.Migrations.Codegen.Nh.Namespace;
            Migrations.Codegen.Nh.Replace = _settings.Migrations.Codegen.Nh.Replace;
            Migrations.Codegen.Nh.Implements = _settings.Migrations.Codegen.Nh.Implements;
            Migrations.Codegen.Nh.Ctor = _settings.Migrations.Codegen.Nh.Ctor;


            //mvc
            Mvc.Authentication.Enabled = _settings.Mvc.Authentication.Enabled;
            Mvc.Authentication.LoginUrl = _settings.Mvc.Authentication.LoginUrl;
            Mvc.Authentication.DefaultUrl = _settings.Mvc.Authentication.DefaultUrl;
            Mvc.Authentication.Global = _settings.Mvc.Authentication.Global;


            _logger.Info("settings loaded");

        }


    }
}
