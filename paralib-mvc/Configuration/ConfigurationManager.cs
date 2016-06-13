using System;
using System.Web.Configuration;
using NET=System.Configuration;
using PARA=com.paralib.Configuration;

namespace com.paralib.Mvc.Configuration
{
    public class ConfigurationManager
    {
        public static NET.Configuration OpenWebConfiguration()
        {
            return WebConfigurationManager.OpenWebConfiguration("~");
        }


        public static void InitializeWebConfig()
        {
            NET.Configuration cfg = OpenWebConfiguration();
            PARA.ConfigurationManager.InitializeConfiguration(cfg);
            PARA.ConfigurationManager.SaveConfiguration(cfg);
        }

    }


 

}
