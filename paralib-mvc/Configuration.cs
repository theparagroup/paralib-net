using System;
using System.Web.Configuration;
using com.paralib.common.Configuration;

namespace com.paralib.mvc
{
    public class Configuration
    {
        public static void Configure()
        {
            System.Configuration.Configuration cfg = WebConfigurationManager.OpenWebConfiguration("~");
            ConfigurationManager.Configure(cfg);
        }

    }


 

}
