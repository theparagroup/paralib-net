using System;
using System.Collections.Generic;
using System.Linq;
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
                            base.OnAuthorization(filterContext);

                            if (filterContext.Result is HttpUnauthorizedResult)
                            {

                                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                                {
                                    string loginUrl = UnauthenticatedUrl;
                                    if (loginUrl == null) loginUrl = Paralib.Mvc.Authentication.LoginUrl;
                                    if (loginUrl == null) loginUrl = FormsAuthentication.LoginUrl;

                                    loginUrl += "?ReturnUrl=" + filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl);
                                    filterContext.Result = new RedirectResult(loginUrl);
                                }
                                else
                                {
                                    filterContext.Result = new RedirectResult(UnauthorizedUrl);
                                }

                            }
                            else
                            {
                                //TODO roles? is this built-in?
                            }


                        }


                    }

                }

            }


        }
    }
}
