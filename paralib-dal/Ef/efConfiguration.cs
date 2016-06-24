﻿using System;
using NET=System.Data.Entity;
using System.Data.Entity.Infrastructure;
using com.paralib;

namespace com.paralib.Dal.Ef
{
    public class EfConfiguration : NET.DbConfiguration
    {
        public EfConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new DefaultExecutionStrategy());
            SetDefaultConnectionFactory(new SqlConnectionFactory(Paralib.Configuration.Dal.ConnectionString));
        }
    }
}
