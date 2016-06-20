﻿using System;
using log4net.Appender;

namespace com.paralib.Logging
{
    public class ParaAdoNetAppender : AdoNetAppender
    {
        public static readonly string DefaultConnectionType = "System.Data.SqlClient.SqlConnection";
        public static readonly string DefaultPattern="%date, %timestamp, %.32thread, %property{tid}, %.16level, %.256logger, %.256property{method}, %.256property{user}, %.256message";
        public static readonly string DefaultTable = "log";
        public static readonly string DefaultFields = "date, timestamp, thread, tid, level, logger, method, user, message";


    }
}