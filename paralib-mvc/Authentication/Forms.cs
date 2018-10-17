using com.paralib.Mvc.Authorization;
using System;
using System.Web;
using NET = System.Web.Security;
using com.paralib.Utils;

namespace com.paralib.Mvc.Authentication
{
    public static class Forms
    {
        public static void Authenticate(string userName, bool? persist = null)
        {
            NET.FormsAuthentication.RedirectFromLoginPage(userName, persist ?? Paralib.Mvc.Authentication.Persist);
        }

        public static void Authenticate(ParaPrinciple principle, string defaultUrl = null, int? timeout = null, bool? persist = null)
        {
            string data = Json.Serialize(principle.Roles);
            Authenticate(principle?.Identity?.Name, data, defaultUrl, timeout, persist);
        }

        public static void Authenticate(string userName, string data, string defaultUrl = null, int? timeout=null, bool? persist=null)
        {
            //cookie will persist across browser sessions - default is to limit to session
            bool createPersistentCookie = persist ?? Paralib.Mvc.Authentication.Persist;

            //Note: FormsAuthentication.SlidingExpiration defaults to true, so the expiration will reset when more than half the time has expired

            //create ticket and encrypt
            NET.FormsAuthenticationTicket ticket= new NET.FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(timeout ?? Paralib.Mvc.Authentication.Timeout), createPersistentCookie, data);
            string encryptedTicket = NET.FormsAuthentication.Encrypt(ticket);

            //save cookie
            HttpCookie cookie = new HttpCookie(NET.FormsAuthentication.FormsCookieName, encryptedTicket);

            if (createPersistentCookie)
            {
                cookie.Expires = ticket.Expiration;
            }

            cookie.Path = NET.FormsAuthentication.FormsCookiePath;

            HttpContext.Current.Response.Cookies.Add(cookie);

            //redirect
            string strRedirectUrl= HttpContext.Current.Request["ReturnUrl"];

            if (strRedirectUrl == null)
            {
                strRedirectUrl = NET.FormsAuthentication.DefaultUrl;
            }

            HttpContext.Current.Response.Redirect(strRedirectUrl, true);
        }

        public static void DeAuthenticate()
        {
            NET.FormsAuthentication.SignOut();
        }


        public static ParaPrinciple GetParaPrinciple(NET.FormsAuthenticationTicket ticket)
        {
            //note: this is called in the ParaMvcModule HttpModule to recreate the ParaPrincipal for each request

            string[] roles = null;

            try
            {
                roles = Json.DeSerialize<string[]>(ticket.UserData);
            }
            catch { }

            return new ParaPrinciple(ticket.Name, roles);
        }

    }
}
