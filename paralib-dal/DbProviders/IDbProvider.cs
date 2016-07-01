using System;
using System.Data;
using com.paralib.Dal.Metadata;

namespace com.paralib.Dal.DbProviders
{
    public interface IDbProvider
    {

        void Open();

        IDataReader ExecuteReader(string sql);
        int ExecuteNonQuery(string sql);
        T ExecuteScalar<T>(string sql);

        Table[] GetTables();
        bool TableExists(string tableName);

        void Close();

        string Encode(string value);

    }
}
