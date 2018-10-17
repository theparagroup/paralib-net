using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace com.paralib.Mvc.Authorization
{
    public class PermissionsAttribute : AuthorizeAttribute
    {
        public bool Anonymous { get; set; }
        public string LoginUrl { get; set; }
        public string UnauthorizedUrl { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (FormsAuthentication.IsEnabled)
            {
                //provide a simple way to bypass authentication/authorization via the attribute
                if (!Anonymous)
                {
                    //respect this on action
                    if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                    {
                        //respect this on controller
                        if (!filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                        {
                            //Note: Roles are built into the framework via IPrincipal and IsInRole()

                            base.OnAuthorization(filterContext);

                            if (filterContext.Result is HttpUnauthorizedResult)
                            {

                                //allow loginUrl to be overridden in attribute
                                string loginUrl = LoginUrl ?? FormsAuthentication.LoginUrl;

                                //allow unauthrorizedUrl to be overridden in attribute
                                string unauthrorizedUrl = UnauthorizedUrl ?? Paralib.Mvc.Authentication.UnauthorizedUrl ?? loginUrl;

                                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                                {
                                    //continue to redirect to login page
                                    loginUrl += "?ReturnUrl=" + filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl);
                                    filterContext.Result = new RedirectResult(loginUrl);
                                }
                                else
                                {
                                    //provide an alternate url for unauthorized in attribute
                                    filterContext.Result = new RedirectResult(unauthrorizedUrl);
                                }

                            }
                        }
                    }
                }
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //we don't want to send a 302 redirect for Ajax calls

            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;
            var user = filterContext.HttpContext.User;

            //"X-Requested-With" == "XMLHttpRequest"
            if (request.IsAjaxRequest())
            {
                if (user.Identity.IsAuthenticated == false)
                {
                    response.StatusCode = (int)HttpStatusCode.Unauthorized; //401
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.Forbidden; //403
                }

                response.SuppressFormsAuthenticationRedirect = true;
                response.End();
            }

            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}
