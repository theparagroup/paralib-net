using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using log4net;
using com.paralib.common;

[assembly: PreApplicationStartMethod(typeof(com.paralib.mvc.PreApplicationStartCode), "Start")]

namespace com.paralib.mvc
{
    public static class PreApplicationStartCode
    {
        private static ILog _logger = LogManager.GetLogger(typeof(PreApplicationStartCode));
        private static readonly object _lock = new object();
        private static bool _executed;

        public static void Start()
        {
            //you won't see this normally
            _logger.Info(null);

            //supposedly Pre-application start code runs in a single thread... but's let's be safe
            lock (_lock)
            {
                if (_executed)
                {
                    return;
                }

                //get logging going early (using web.config)
                Paralib.Initialize();

                //register our paralib MVC module
                DynamicModuleUtility.RegisterModule(typeof(ParaMvcModule));


                _executed = true;
                _logger.Info("pre-application start code executed");

            }

        }

    }
}
