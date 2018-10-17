using System;

namespace com.paralib.ParalibProperties.Mvc
{
    public class AuthenticationProperties
    {
        public bool Enabled { get; internal set; }
        public string LoginUrl { get; internal set; }
        public string UnauthorizedUrl { get; internal set; }
        public string DefaultUrl { get; internal set; }
        public bool Global { get; internal set; }
        public int Timeout { get; internal set; }
    }
}
