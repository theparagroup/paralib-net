using System;
using System.Configuration;

namespace com.paralib.Configuration.Migrations.Codegen
{

    public class TableElement : ConfigurationElement
    {

        [ConfigurationProperty("name")]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }


    }

}
