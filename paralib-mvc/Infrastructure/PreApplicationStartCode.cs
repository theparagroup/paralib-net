using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Web.Security;
using System.Collections.Specialized;
using com.paralib;

[assembly: PreApplicationStartMethod(typeof(com.paralib.Mvc.Infrastructure.PreApplicationStartCode), "Start")]

namespace com.paralib.Mvc.Infrastructure
{
    public static class PreApplicationStartCode
    {
        private static ILog _logger = Paralib.GetLogger(typeof(PreApplicationStartCode));
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

                //if (using authentication) configure FormsAuthentication (has to be done here)
                //note: this can't be reversed, so the Configure event can't turn it off
                if (Paralib.Mvc.Authentication.Enabled)
                {
                    NameValueCollection forms = new NameValueCollection();
                    if (Paralib.Mvc.Authentication.LoginUrl!=null) forms.Add("loginUrl", Paralib.Mvc.Authentication.LoginUrl);
                    if (Paralib.Mvc.Authentication.DefaultUrl != null) forms.Add("defaultUrl", Paralib.Mvc.Authentication.DefaultUrl);

                    FormsAuthentication.EnableFormsAuthentication(forms);
                }

                //you can do this, but intellisense won't work
                //System.Web.WebPages.Razor.WebPageRazorHost.AddGlobalImport("com.paralib.Mvc.Infrastructure.ExtensionMethods");


                //register our paralib MVC module (which will continue configuring things)
                DynamicModuleUtility.RegisterModule(typeof(ParaMvcModule));

                _executed = true;
                _logger.Info("pre-application start code executed");

            }

        }

    }
}
