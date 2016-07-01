using System;
using System.Configuration;

namespace com.paralib.Configuration.Migrations.Codegen
{

    public class NhElement : GenerationOptionsBaseElement
    {

        [ConfigurationProperty("replace", DefaultValue = true)]
        public bool Replace
        {
            get { return (bool)base["replace"]; }
            set { base["replace"] = value; }
        }


    }

}
