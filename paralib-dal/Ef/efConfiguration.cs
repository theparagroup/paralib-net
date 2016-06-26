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
            _log.Info($"Reverting to default EF Execution Strategy.");
            SetExecutionStrategy("System.Data.SqlClient", () => new DefaultExecutionStrategy());
            _log.Info($"Clearing default EF Connection Factory.");
            SetDefaultConnectionFactory(new SqlConnectionFactory("Server=0.0.0.0,0;Integrated Security=SSPI;"));
        }
    }
}
