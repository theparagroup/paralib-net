using System;

namespace com.paralib.SettingsOptions.Mvc
{
    public class MvcOptions
    {
        public AuthenticationOptions Authentication { get; set; } = new AuthenticationOptions();
    }
}
