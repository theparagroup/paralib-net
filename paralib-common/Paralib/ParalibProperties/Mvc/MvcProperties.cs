using System;

namespace com.paralib.ParalibProperties.Mvc
{
    public class MvcProperties
    {
        public AuthenticationProperties Authentication { get; internal set; } = new AuthenticationProperties();
    }
}
