using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: PreApplicationStartMethod(typeof(com.paralib.mvc.PreApplicationStartCode), "Start")]

namespace com.paralib.mvc
{
    public static class PreApplicationStartCode
    {
        private static readonly object _lock = new object();
        private static bool _started;

        public static void Start()
        {
            //supposedly Pre-application start code runs in a single thread... but's let's be safe
            lock (_lock)
            {
                if (_started)
                {
                    return;
                }

                DynamicModuleUtility.RegisterModule(typeof(ParaMvcModule));

                _started = true;

            }

        }

    }
}
