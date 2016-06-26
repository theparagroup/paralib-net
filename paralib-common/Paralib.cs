using System;
using com.paralib.Configuration;
using com.paralib.Logging;
using com.paralib.Ado;

namespace com.paralib
{

    public static partial class Paralib
    {
        private static ILog _logger = GetLogger(typeof(Paralib));
        private static readonly object _lock = new object();
        private static bool _initialized;
        private static Settings _settings = new Settings();

        /* 
        
            All applications call one of these Initialize methods to configure the
            Paralib. The Paralib will work without being initialized, but only with
            default settings, which doesn't do very much.

        */

        public static void Initialize()
        {
            //use a config file (app/web or paralib)
            _logger.Info("initializing with config file");
            Initialize(Settings.Create(ConfigurationManager.ParalibSection));
        }

        public static void Initialize(Action<Settings> settings)
        {
            //use the action syntax
            _logger.Info("initializing with action syntax");
            Settings s = new Settings();
            settings(s);
            Initialize(s);
        }

        public static void Initialize(Settings settings)
        {
            //explicit
            _logger.Info("initializing with explicit settings");

            lock (_lock)
            {
                if (_initialized)
                {
                    return;
                }

                //initital setup of the paralib
                _settings = settings;
                Load();

                _initialized = true;

                _logger.Info("paralib initialized");

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
            _logger.Info("loading settings...");

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

            //dal

            //mvc

            _logger.Info("settings loaded");

        }


    }
}
