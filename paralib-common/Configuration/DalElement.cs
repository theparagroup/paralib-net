using System;
using System.Configuration;

namespace com.paralib.Configuration
{

    public class DalElement : ConfigurationElement
    {


        [ConfigurationProperty("databases")]
        [ConfigurationCollection(typeof(DatabasesCollection), AddItemName = "database")]
        public DatabasesCollection Databases
        {
            get { return ((DatabasesCollection)(base["databases"])); }
            set { base["databases"] = value; }
        }


    }

}
