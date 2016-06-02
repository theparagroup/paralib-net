using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace com.paralib.dal
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
                return ConfigurationManager.ConnectionStrings["dbcon"].ToString();
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
