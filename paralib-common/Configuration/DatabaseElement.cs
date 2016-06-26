using System;
using System.Configuration;
using System.ComponentModel;
using com.paralib.Ado;

namespace com.paralib.Configuration
{
    public class DatabaseElement : ConfigurationElement
    {

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        [TypeConverter(typeof(CaseInsensitiveEnum<DatabaseTypes>))]
        public DatabaseTypes DatabaseType
        {
            get { return (DatabaseTypes)base["type"]; }
            set { base["type"] = value; }
        }

        [ConfigurationProperty("server", IsRequired = true)]
        public string Server
        {
            get { return (string)base["server"]; }
            set { base["server"] = value; }
        }

        [ConfigurationProperty("store")]
        public string Store
        {
            get { return (string)base["store"]; }
            set { base["store"] = value; }
        }

        [ConfigurationProperty("port",DefaultValue =null)]
        public int? Port
        {
            get { return (int?)base["port"]; }
            set { base["port"] = value; }
        }

        [ConfigurationProperty("user")]
        public string UserName
        {
            get { return (string)base["user"]; }
            set { base["user"] = value; }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get { return (string)base["password"]; }
            set { base["password"] = value; }
        }

        [ConfigurationProperty("integrated", DefaultValue =false)]
        public bool Integrated
        {
            get { return (bool)base["integrated"]; }
            set { base["integrated"] = value; }
        }

        [ConfigurationProperty("parameters")]
        public string Parameters
        {
            get { return (string)base["parameters"]; }
            set { base["parameters"] = value; }
        }



    }
}
