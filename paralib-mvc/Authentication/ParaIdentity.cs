using System;
using System.Security.Principal;

namespace com.paralib.Mvc.Authentication
{
    public class ParaIdentity : IIdentity
    {

        public string AuthenticationType { get; } = "Paralib";

        public bool IsAuthenticated =>  !string.IsNullOrEmpty(Name); 

        public string Name { get; set; }

        public ParaIdentity(string userName)
        {
            Name = userName;
        }

    }
}
