using System;

namespace com.paralib.Ado
{
    public class Database
    {
        public string Name { get; private set; }
        public DatabaseTypes DatabaseType { get; private set; }
        public string Server { get; set; }
        public int? Port { get; set; }
        public string Store { get; set; }
        public bool Integrated { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Parameters { get; set; }

        public Database(string name, DatabaseTypes databaseType)
        {
            Name = name;
            DatabaseType = databaseType;
        }

        public string GetConnectionString(bool includePassword)
        {

            switch (DatabaseType)
            {
                case DatabaseTypes.SqlServer:
                    return GetSqlServerConnectionString(includePassword);
                case DatabaseTypes.MySql:
                    return GetMySqlConnectionString(includePassword);
                default:
                    throw new ParalibException($"Connection provider '{DatabaseType}' not supported.");
            }
        }


        private string GetSqlServerConnectionString(bool includePassword)
        {
            //sqlserver
            //Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;
            //Trusted_Connection=True;
            //Server=myServerName\myInstanceName
            //Data Source=190.190.200.100,1433;Network Library=DBMSSOCN;
            //Initial Catalog = myDataBase;
            //Integrated Security=true;
            //Integrated Security=SSPI;User ID = myDomain\myUsername;Password=myPassword;

            string server;
            string store=$"Initial Catalog={Store};";
            string credentials="";

            if (Port.HasValue)
            {
                server =$"Data Source={Server},{Port};Network Library=DBMSSOCN;";
            }
            else
            {
                server=$"Data Source={Server};";
            }

            if (Integrated)
            {
                credentials = $"Integrated Security=SSPI;";
            }

            if (UserName != null)
            {
                credentials += $"User ID={UserName};";
            }

            if (Password != null)
            {
                if (includePassword)
                {
                    credentials += $"Password={Password};";
                }
                else
                {
                    credentials += "Password=*****;";
                }
            }


            return $"{server}{store}{credentials}{Parameters}";
        }

        private string GetMySqlConnectionString(bool includePassword)
        {
            //mysql
            //Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;
            //SslMode=Required|Preferred;
            //Port=1234;
            //IntegratedSecurity=yes; Uid=auth_windows;

            string server=$"Server={Server};";
            string store = $"Database={Store};";
            string credentials = "";

            if (Port.HasValue)
            {
                server += $"Port={Port};";
            }

            if (Integrated)
            {
                credentials += $"IntegratedSecurity=yes;";
            }

            if (UserName != null)
            {
                credentials += $"Uid={UserName};";
            }

            if (Password != null)
            {
                if (includePassword)
                {
                    credentials += $"Pwd={Password};";
                }
                else
                {
                    credentials += "Pwd=*****;";
                }
            }


            return $"{server}{store}{credentials}{Parameters}";
        }


        public string ProviderName
        {

            get
            {
                switch (DatabaseType)
                {
                    case DatabaseTypes.SqlServer:
                        return "System.Data.SqlClient";

                    case DatabaseTypes.MySql:
                        return "MySql.Data.MySqlClient";

                    default:
                        throw new ParalibException($"Connection provider '{DatabaseType}' not supported.");
                }

            }

        }

        public string ConnectionType
        {
            get
            {
                //System.Data.OleDb.OleDbConnection, System.Data
                //MySql.Data.MySqlClient.MySqlConnection, MySql.Data
                //System.Data.SqlClient.SqlConnection, System.Data
                //Microsoft.Data.Odbc.OdbcConnection,Microsoft.Data.Odbc
                //System.Data.OracleClient.OracleConnection, System.Data.OracleClient


                switch (DatabaseType)
                {
                    case DatabaseTypes.SqlServer:
                        return $"{ProviderName}.SqlConnection, {AssemblyName}";

                    case DatabaseTypes.MySql:
                        return $"{ProviderName}.MySqlConnection, {AssemblyName}";

                    default:
                        throw new ParalibException($"Connection provider '{DatabaseType}' not supported.");
                }

            }
        }

        public string AssemblyName
        {
            get
            {
                switch (DatabaseType)
                {
                    case DatabaseTypes.SqlServer:
                        return "System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

                    case DatabaseTypes.MySql:
                        return "MySql.Data";

                    default:
                        throw new ParalibException($"Connection provider '{DatabaseType}' not supported.");
                }

            }
        }


    }
}
