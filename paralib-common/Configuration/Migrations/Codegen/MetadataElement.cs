using System;
using System.Configuration;

namespace com.paralib.Configuration.Migrations.Codegen
{

    public class MetadataElement : GenerationOptionsBaseElement
    {

        [ConfigurationProperty("replace", DefaultValue = false)]
        public bool Replace
        {
            get { return (bool)base["replace"]; }
            set { base["replace"] = value; }
        }



    }

}
