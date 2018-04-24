using com.paralib.Mvc.Authorization;
using System;
using System.Web;
using NET = System.Web.Security;
using com.paralib.Utils;

namespace com.paralib.Mvc.Authentication
{
    public static class Forms
    {
        public static void Authenticate(string userName)
        {
            NET.FormsAuthentication.RedirectFromLoginPage(userName, false);
        }

        public static void Authenticate(ParaPrinciple principle, string defaultUrl = null)
        {
            string data = Json.Serialize(principle.Roles);
            Authenticate(principle?.Identity?.Name, data, defaultUrl);
        }

        public static void Authenticate(string userName, string data, string defaultUrl = null)
        {
            //TODO make this an option
            bool persistCookie = false;

            //create ticket and encrypt
            NET.FormsAuthenticationTicket ticket= new NET.FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(60), persistCookie, data);
            string encryptedTicket = NET.FormsAuthentication.Encrypt(ticket);

            //save cookie
            HttpCookie cookie = new HttpCookie(NET.FormsAuthentication.FormsCookieName, encryptedTicket);

            if (persistCookie)
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
