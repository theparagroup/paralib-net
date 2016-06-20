using System;
using log4net.Appender;

namespace com.paralib.Logging
{
    public class ParaAdoNetAppender : AdoNetAppender
    {
        public static readonly string DefaultPattern="";

        public ParaAdoNetAppender()
        {
            ConnectionString = Paralib.Configuration.Dal.ConnectionString;
        }
    }
}