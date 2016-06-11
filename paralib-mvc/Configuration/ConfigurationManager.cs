using System;
using System.Web.Configuration;
using NET=System.Configuration;
using PARA=com.paralib.common.Configuration;

namespace com.paralib.mvc.Configuration
{
    public class ConfigurationManager
    {
        public static void InitializeWebConfig()
        {
            NET.Configuration cfg = WebConfigurationManager.OpenWebConfiguration("~");
            PARA.ConfigurationManager. CreateParalibSection(cfg);
            //cfg.Save(NET.ConfigurationSaveMode.Full, true);
        }

    }


 

}
