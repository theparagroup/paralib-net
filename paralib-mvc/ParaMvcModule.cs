using System;
using System.Web;
using log4net;

namespace com.paralib.mvc
{
    public class ParaMvcModule : IHttpModule
    {
        private static ILog _logger = LogManager.GetLogger(typeof(ParaMvcModule));
        private static readonly object _lock = new object();
        private static bool _started;

        public void Dispose()
        {
            _logger.Info(null);
        }

        public void Init(HttpApplication context)
        {
            _logger.Info(null);

            //init is called for each instance of HttpApplication, and there can be many
            //per Application Domain as HttpApplication objects may be pooled.
            lock (_lock)
            {
                if (_started)
                {
                    return;
                }

                //create web section if needed

                //load config/delta

                _logger.Info("ParaMvcModule start up");

                _started = true;

            }



        }
    }
}
