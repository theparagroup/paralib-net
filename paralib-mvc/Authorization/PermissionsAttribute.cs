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
        public string UnauthenticatedUrl { get; set; }
        public string UnauthorizedUrl { get; set; }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (Paralib.Mvc.Authentication.Enabled)
            {
                if (!Anonymous)
                {
                    if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                    {
                        if (!filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                        {
                            //Note: Roles are built into the framework via IPrincipal and IsInRole()

                            base.OnAuthorization(filterContext);

                            if (filterContext.Result is HttpUnauthorizedResult)
                            {
                                string loginUrl = UnauthenticatedUrl;
                                if (loginUrl == null) loginUrl = Paralib.Mvc.Authentication.LoginUrl;
                                if (loginUrl == null) loginUrl = FormsAuthentication.LoginUrl;

                                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                                {
                                    loginUrl += "?ReturnUrl=" + filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl);
                                    filterContext.Result = new RedirectResult(loginUrl);
                                }
                                else
                                {
                                    filterContext.Result = new RedirectResult(UnauthorizedUrl ?? loginUrl);
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
