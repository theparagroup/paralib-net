using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using com.paralib;
using com.paralib.Mvc.Configuration;
using System.Web.Security;
using System.Security.Principal;
using com.paralib.Mvc.Authorization;
using com.paralib.Mvc.Authentication;
using WebApi = System.Web.Http;
using static System.Web.Http.HttpConfigurationExtensions;
using com.paralib.Mvc.Infrastructure.WebApi2;

namespace com.paralib.Mvc.Infrastructure
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
                if (!_initialized)
                {

                    //create a default web.config section & connectionstring if they don't exist
                    //(paralib was already initialized in PreApplicationStartCode using web.config)
                    ConfigurationManager.InitializeWebConfig();

                    //allow configuration to be modified programatically 
                    //(probably via a static handler in global.asax)
                    //TODO shouldn't this be called in the Paralib.Initialize() method automatically
                    Paralib.RaiseConfigureEvent();

                    //configure MVC
                    RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
                    RouteTable.Routes.MapMvcAttributeRoutes();

                    //configure WebApi2
                    //TODO this should execute only if webapi2 is used/enabled?
                    try
                    {
                        WebApi.GlobalConfiguration.Configure(config => 
                        {
                            config.MapHttpAttributeRoutes(new ParaDirectRouteProvider());
                            config.Services.Add(typeof(WebApi.ExceptionHandling.IExceptionLogger), new ParaExceptionLogger());
                        });
                    }
                    catch { }

                    //if (using authentication) configure MVC authentication
                    if (Paralib.Mvc.Authentication.Enabled)
                    {
                        //if glogal=true put "[Authorize]" on everything
                        if (Paralib.Mvc.Authentication.Global)
                        {
                            GlobalFilters.Filters.Add(new AuthorizeAttribute());
                        }
                    }

                    _initialized = true;

                    _logger.Info("module first-request initialized");
                }


            }

            //in integrated mode, init is called repeatedly and for some reason if you
            //things blow up if you do this above:
            //
            //      if (_initialized) return;
            //
            //my guess if you attach the handler to one instance, apparently all instances need 
            //the handler as well. no problem in classic mode.
            //
            context.PostAuthenticateRequest += Context_PostAuthenticateRequest;
            context.BeginRequest += new EventHandler(Context_BeginRequest);


        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {
            _logger.Info(((HttpApplication)sender).Context.Request.Url);
        }

        private void Context_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;

            if (Paralib.Mvc.Authentication.Enabled)
            {

                if (FormsAuthentication.CookiesSupported == true)
                {
                    if (context.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                    {
                        try
                        {
                            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(context.Request.Cookies[FormsAuthentication.FormsCookieName].Value);

                            IPrincipal principle = Forms.GetParaPrinciple(ticket);

                            if (principle == null)
                            {
                                principle = new GenericPrincipal(new GenericIdentity(null, null), null);
                            }

                            HttpContext.Current.User = principle;
                            System.Threading.Thread.CurrentPrincipal = principle;

                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }


    }
}
