using System;
using System.Data.SqlClient;
using com.paralib;

namespace com.paralib.Dal
{
    public class Db
    {
        private SqlConnection _con;
        private SqlCommand _cmd;

        public static string Encode(string value)
        {
            if (value == null)
            { 
                return "NULL";
            }
            else
            {
                 return "'" + value.Replace("'", "''") + "'";
            }
            
        }


        public static String ConnectionString
        {
            get
            {
                return Paralib.Dal.Database.GetConnectionString(true);
            }
        }

        public Db(bool autoOpen=true, string connectionString=null)
        {
            if (autoOpen)
            {
                Open(connectionString);
            }
        }

        public void Open(string connectionString)
        {
            if (connectionString==null)
            {
                connectionString = ConnectionString;
            }

            if (connectionString==null)
            {
                throw new ParalibException("Db class requires a valid ConnectionString. Provide one explicitly or configure a default in the <paralib> configuration section.");
            }

            _con = new SqlConnection(connectionString);
            _con.Open();
        }

        public void Open()
        {
            Open(null);
        }

        public System.Data.IDataReader ExecuteReader(string sql)
        {
            //caller must close reader
            _cmd = _con.CreateCommand();
            _cmd.CommandText = sql;
            return _cmd.ExecuteReader();
        }

        public int ExecuteNonQuery(string sql)
        {
            //caller must close reader
            _cmd = _con.CreateCommand();
            _cmd.CommandText = sql;
            return _cmd.ExecuteNonQuery();
        }

        public int ExecuteScalar(string sql)
        {
            _cmd = _con.CreateCommand();
            _cmd.CommandText = sql;
            return (int)_cmd.ExecuteScalar();
        }

        public void Close()
        {
            //no need to close or dispose SqlCommand
            _con.Close();
        }
    }
}
