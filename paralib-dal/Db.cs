using System;
using System.Data;
using com.paralib.Ado;
using com.paralib.Dal.DbProviders;
using com.paralib.Dal.Metadata;

namespace com.paralib.Dal
{
    public class Db:IDbProvider,IDisposable
    {
        private Database _database;
        private IDbProvider _provider;

        public Db(string database = null, bool autoOpen = true):this(Paralib.Dal.Databases[database], autoOpen)
        {
        }

        public Db(Database database, bool autoOpen=true)
        {
            if (database==null)
            {
                throw new ParalibException("Datbase cannot be null.");
            }

            _database = database;

            switch (_database.DatabaseType)
            {
                case DatabaseTypes.SqlServer:
                    _provider = new SqlServerDbProvider(_database.GetConnectionString(true));
                    break;

                default:
                    throw new ParalibException($"Database type {_database.DatabaseType} not supported.");
            }


            if (autoOpen)
            {
                Open();
            }
        }

        public void Open()
        {
            _provider.Open();
        }

        public void Close()
        {
            _provider.Close();
        }

        public string Encode(string value)
        {
            return _provider.Encode(value);
        }

        public int ExecuteNonQuery(string sql)
        {
            return _provider.ExecuteNonQuery(sql);
        }

        public IDataReader ExecuteReader(string sql)
        {
            return _provider.ExecuteReader(sql);
        }

        public T ExecuteScalar<T>(string sql)
        {
            return _provider.ExecuteScalar<T>(sql);
        }

        public Table[] GetTables()
        {
            return _provider.GetTables();
        }

        public bool TableExists(string tableName)
        {
            return _provider.TableExists(tableName);
        }

        public void Dispose()
        {
            _provider.Close();
        }
    }
}
