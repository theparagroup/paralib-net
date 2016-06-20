using System;
using System.Web;
using com.paralib;
using com.paralib.Mvc.Configuration;

namespace com.paralib.Mvc
{
    public class ParaMvcModule : IHttpModule
    {
        private static ILog _logger = Paralib.GetLogger(typeof(ParaMvcModule));
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

            //requests are multi-threaded - we want the first one to get here
            lock (_lock)
            {
                if (_initialized)
                {
                    return;
                }

                //create a default web.config section & connectionstring if they don't exist
                //(paralib was already initialized in PreApplicationStartCode using web.config)
                ConfigurationManager.InitializeWebConfig();

                //allow configuration to be modified programatically 
                //(probably via a static handler in global.asax)
                Paralib.RaiseConfigureEvent();

                _initialized = true;

                _logger.Info("module first-request initialized");

            }
        }


    }
}
