using System;
using System.Configuration;

namespace com.paralib.Configuration
{

    public class ParalibSection : ConfigurationSection
    {

        /*
            Note: we use "sane" defaults (off, false, none).

        */

        [ConfigurationProperty("logging")]
        public LoggingElement Logging
        {
            get { return ((LoggingElement)(base["logging"])); }
            set { base["logging"] = value; }
        }

        [ConfigurationProperty("dal")]
        public DalElement Dal
        {
            get { return ((DalElement)(base["dal"])); }
            set { base["dal"] = value; }
        }

    }
}
