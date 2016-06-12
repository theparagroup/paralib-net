using System;
using log4net;
using com.paralib.common.Configuration;

namespace com.paralib.common
{
    public static class Paralib
    {
        private static ILog _logger = LogManager.GetLogger(typeof(Paralib));
        private static readonly object _lock = new object();
        private static bool _initialized;
        private static Settings _settings;

        /*
        
            SettingsChanged Event

            This is a static event, so make sure you unsubcribe your instances.

            This event does not follow the standard EventHandler pattern.

            Just for fun, we use explicit add/remove event methods.

        */
        private static event SettingsChangedEventHandler _settingsChanged;

        public static event SettingsChangedEventHandler SettingsChanged
        {
            add
            {
                _settingsChanged += value;
            }
            remove
            {
                _settingsChanged -= value;
            }
        }

        public static void Initialize()
        {
            Initialize(Settings.Load(ConfigurationManager.GetParalibSection()));
        }

        public static void Initialize(Action<Settings> settings)
        {
            Settings s = new Settings();
            settings(s);
            Initialize(s);
        }

        public static void Initialize(Settings settings)
        {
            _logger.Info(null);

            lock (_lock)
            {
                if (_initialized)
                {
                    return;
                }

                //save initial settings
                _settings = settings;

                //let's set up the paralib
                Setup(settings);


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


        public static void Setup(Settings settings)
        {

            //DAL
            Configuration.Dal.Connection = settings.Dal.Connection;
            Configuration.Dal.ConnectionString = settings.Dal.ConnectionString;

            //if logging
            Logging.LoggingConfiguration.Configure(Logging.LoggingModes.Mvc);

            if (_settingsChanged != null)
            {
                _settingsChanged(settings);
            }

            _logger.Info("settings changed");
    }

    public static class Configuration
        {

            public static class Dal
            {
                public static string Connection { get; internal set; }
                public static string ConnectionString { get; internal set; }
            }
        }





    }
}
