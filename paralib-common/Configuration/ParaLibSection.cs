using System;
using System.Configuration;

namespace com.paralib.common.Configuration
{
    /*
        <paralib>

            <common>
                <logging enabled="false|true" debug="false|true" level="OFF|FATAL" logUser="false|true">
                    <loggers>
                    <logger name="file" threshold="OFF|FATAL" filename="application.log"/>
                    <logger name="database" threshold="OFF|FATAL" connection="<empty>|mvc" connectionType="System.Data.SqlClient.SqlConnection"/>
                    </loggers>
                </logging>
            </common>

            <dal connection="oovent" />

            <mvc>
                <authentication/>
                <authorization/>
            </mvc>
    
        </paralib>
    */




    public class ParalibSection : ConfigurationSection
    {
        [ConfigurationProperty("dal")]
        public DalElement Dal
        {
            get { return ((DalElement)(base["dal"])); }
            set { base["dal"] = value; }
        }

    }
}
