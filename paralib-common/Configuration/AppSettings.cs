using System;

namespace com.paralib.Configuration
{
    public class AppSettings
    {
        public string this[string name]
        {
            get
            {
                return ConfigurationManager.GetAppSetting(name);
            }
        }
    }
}
