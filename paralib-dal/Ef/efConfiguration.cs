using System;
using NET=System.Data.Entity;
using System.Data.Entity.Infrastructure;
using com.paralib;

namespace com.paralib.Dal.Ef
{
    public class EfConfiguration : NET.DbConfiguration
    {
        private static ILog _log = Paralib.GetLogger(typeof(EfConfiguration));

        public EfConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new DefaultExecutionStrategy());
            SetDefaultConnectionFactory(new SqlConnectionFactory(Paralib.Dal.Database.GetConnectionString(true)));
            _log.Info($"Setting default connection to '{Paralib.Dal.Database.Name}' = '{Paralib.Dal.Database.GetConnectionString(false)}'");
        }
    }
}
