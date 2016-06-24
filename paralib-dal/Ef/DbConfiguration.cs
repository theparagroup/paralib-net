using System;
using NET=System.Data.Entity;
using System.Data.Entity.Infrastructure;
using com.paralib;

namespace com.paralib.Dal.Ef
{
    public class DbConfiguration : NET.DbConfiguration
    {
        public DbConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new DefaultExecutionStrategy());
            SetDefaultConnectionFactory(new SqlConnectionFactory(Paralib.Configuration.Dal.ConnectionString));
        }
    }
}
