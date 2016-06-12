using System;
using System.Web;
using log4net;
using com.paralib.common;
using com.paralib.mvc.Configuration;

namespace com.paralib.mvc
{
    public class ParaMvcModule : IHttpModule
    {
        private static ILog _logger = LogManager.GetLogger(typeof(ParaMvcModule));
        private static readonly object _lock = new object();
        private static bool _initialized;


        public ParaMvcModule()
        {
            _logger.Info(".ctor");
        }

        public void Dispose()
        {
            _logger.Info(null);
        }

        public void Init(HttpApplication context)
        {
            _logger.Info(null);

            //requests are multi-threaded - we want the f
            lock (_lock)
            {
                if (_initialized)
                {
                    return;
                }

                //create a default web.config section & connectionstring if they don't exist
                ConfigurationManager.InitializeWebConfig();

                //setup paralib again (allow global.asax to modify configuration)
                Paralib.Setup(Paralib.Settings);


                _initialized = true;

                _logger.Info("module first-request initialized");

            }
        }


    }
}
