using System;
using log4net.Appender;

namespace com.paralib.Logging
{
    public class ParaAdoNetAppender : AdoNetAppender
    {
        public static readonly string DefaultConnectionType = "System.Data.SqlClient.SqlConnection";
        public static readonly string DefaultPattern="%date, %.50level, %.255logger, %.255property{method}, %.255property{user}, %.4000message";
        public static readonly string DefaultTable = "log";
        public static readonly string DefaultFields = "date, level, logger, method, user, message";


    }
}