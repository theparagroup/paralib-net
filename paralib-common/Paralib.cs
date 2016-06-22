using System;
using com.paralib.Configuration;
using com.paralib.Logging;

namespace com.paralib
{

    public static partial class Paralib
    {
        private static ILog _logger = GetLogger(typeof(Paralib));
        private static readonly object _lock = new object();
        private static bool _initialized;
        private static Settings _settings;


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
                lock(_lock)
                {
                    _configureEvent += value;
                }
            }
            remove
            {
                lock(_lock)
                {
                    _configureEvent -= value;
                }
            }
        }

        public static void RaiseConfigureEvent()
        {
            //allow library consumers to modify configuration programatically

            ConfigureEventHandler handler;

            lock(_lock)
            {
                handler = _configureEvent;
            }

            if (handler != null)
            {
                _logger.Info("configure event raised");


                //send handlers the stored settings object to modify
                handler(_settings);

                //call setup again
                Setup();
            }

        }


        public static void Initialize()
        {
            Initialize(Settings.Load(ConfigurationManager.ParalibSection));
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

                //initital setup of the paralib
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

        private static void Setup(Settings settings)
        {
            _settings = settings;
            Setup();
        }

        public static void Setup()
        {
            Configuration.Load(_settings);

            _logger.Info("settings changed");
        }

        public static DataAnnotations.ParaTypes ParaTypes
        {
            get
            {
                return DataAnnotations.ParaTypes.Instance;
            }
        }

    }
}
