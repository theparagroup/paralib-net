using System;
using System.Configuration;

namespace com.paralib.Configuration
{

    public class ParalibSection : ConfigurationSection
    {

        /*
            Note: we use "sane" defaults (off, false, none), except for things that are
            implied to be "on" if they are present.

            For example:

                <paralib/> (logging off)

                <paralib><logging/></paralib> (logging enabled)

                <paralib><logging><log/></logging></paralib> (logging enabled, log enabled)


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
