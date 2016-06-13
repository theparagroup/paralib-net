using System;
using System.Web.Configuration;
using NET=System.Configuration;
using PARA=com.paralib.Configuration;

namespace com.paralib.Mvc.Configuration
{
    public class ConfigurationManager
    {
        public static void InitializeWebConfig()
        {
            NET.Configuration cfg = WebConfigurationManager.OpenWebConfiguration("~");
            PARA.ConfigurationManager.CreateParalibSection(cfg);
            PARA.ConfigurationManager.Save(cfg);
        }

    }


 

}
