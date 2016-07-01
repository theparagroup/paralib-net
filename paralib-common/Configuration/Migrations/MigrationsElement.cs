using System;
using System.Configuration;
using com.paralib.Configuration.Migrations.Codegen;

namespace com.paralib.Configuration.Migrations
{

    public class MigrationsElement : ConfigurationElement
    {

        [ConfigurationProperty("devmode", DefaultValue =false)]
        public bool Devmode
        {
            get { return (bool)base["devmode"]; }
            set { base["devmode"] = value; }
        }

        [ConfigurationProperty("database")]
        public string Database
        {
            get { return (string)base["database"]; }
            set { base["database"] = value; }
        }

        [ConfigurationProperty("commands")]
        public string Commands
        {
            get { return (string)base["commands"]; }
            set { base["commands"] = value; }
        }

        [ConfigurationProperty("codegen")]
        public CodegenElement Codegen
        {
            get { return ((CodegenElement)(base["codegen"])); }
            set { base["codegen"] = value; }
        }

    }

}
