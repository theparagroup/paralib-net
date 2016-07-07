using System;
using System.Linq;
using System.Security.Principal;
using com.paralib.Mvc.Authentication;

namespace com.paralib.Mvc.Authorization
{
    public class ParaPrinciple : IPrincipal
    {
        public IIdentity Identity { get; set; }
        public string[] Roles { get; set; }


        public ParaPrinciple(string userName, string[] roles)
        {
            Identity = new ParaIdentity(userName);
            Roles = roles;
        }

        public bool IsInRole(string role)
        {
            if (Roles!=null)
            {
                return ((from r in Roles where r==role select r).Count()>0);
            }

            return false;
        }

    }
}
