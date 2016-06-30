using System;
using System.Data;
using System.Data.SqlClient;

namespace com.paralib.Dal.DbProviders
{
    public class SqlServerDbProvider : DbProviderBase
    {
        public SqlServerDbProvider(string connectionString):base(connectionString)
        {

        }

        public override IDbConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
