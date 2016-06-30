using System;
using System.Collections.Generic;
using System.Data;

namespace com.paralib.Dal.DbProviders
{
    public abstract class DbProviderBase : IDbProvider
    {
        protected IDbConnection _con;

        public DbProviderBase(string connectionString)
        {
            _con = CreateConnection(connectionString);
        }

        public abstract IDbConnection CreateConnection(string connectionString);

        public virtual void Open()
        {
            _con.Open();
        }

        public virtual void Close()
        {
            //no need to close or dispose SqlCommand
            _con.Close();
        }

        public virtual IDataReader ExecuteReader(string sql)
        {
            IDbCommand cmd = _con.CreateCommand();
            cmd.CommandText = sql;
            return cmd.ExecuteReader();
        }

        public virtual int ExecuteNonQuery(string sql)
        {
            //caller must close reader
            IDbCommand cmd = _con.CreateCommand();
            cmd.CommandText = sql;
            return cmd.ExecuteNonQuery();
        }

        public virtual T ExecuteScalar<T>(string sql)
        {
            IDbCommand cmd = _con.CreateCommand();
            cmd.CommandText = sql;
            return (T) cmd.ExecuteScalar();
        }

        public virtual string Encode(string value)
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

        public virtual Table[] GetTables()
        {
            var reader = ExecuteReader("select TABLE_NAME from INFORMATION_SCHEMA.TABLES");

            List<Table> ts = new List<Table>();

            while(reader.Read())
            {
                var table=new Table();
                table.Name = reader.GetValue<string>("TABLE_NAME");

                ts.Add(table);
            }

            reader.Close();

            foreach (var t in ts)
            {
                AddColumns(t);
            }

            return ts.ToArray();

        }

        protected virtual void AddColumns(Table table)
        {
            string sql = "select TABLE_NAME, COLUMN_NAME, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE";
            sql += $" from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='{table.Name}' order by ORDINAL_POSITION";

            var reader = ExecuteReader(sql);
             
            table.Columns = new Dictionary<string,Column>();
            int ordinal = 0;

            while (reader.Read())
            {
                Column column = new Column();

                column.Name = reader.GetValue<string>("COLUMN_NAME");

                column.Ordinal = ordinal;
                ++ordinal;

                column.IsNullable = reader.GetValue<string>("IS_NULLABLE")=="YES"? true : false;
                column.DbType = reader.GetValue<string>("DATA_TYPE");

                column.Length = reader.GetValue<int?>("CHARACTER_MAXIMUM_LENGTH");
                column.Precision = reader.GetValue<byte?>("NUMERIC_PRECISION");
                column.Scale = reader.GetValue<int?>("NUMERIC_SCALE");

                table.Columns.Add(column.Name,column);
            }

            reader.Close();


            AddClrTypes(table);
            FindPrimaryKeys(table);
            FindRelationships(table);

        }

        private void _AddClrTypes(Table table)
        {
            /*

                This version will read the first row of data on SqlServer and gets the datatype
                at runtime. Handy to detirmine the mappings below.

            */
            var reader = ExecuteReader($"select top 1 * from {table.Name}");

            if (reader.Read())
            {
                foreach (var c in table.Columns.Values)
                {
                    c.ClrType = reader[c.Name].GetType();
                }

            }

            reader.Close();

        }

        protected virtual void AddClrTypes(Table table)
        {
            /*
                Fluent, DbType, ClrType
                -------------------------------------------------
                Asid, int, System.Int32
                AsAnsiString, varchar, System.String
                AsBinary, varbinary, System.Byte[]
                AsBoolean, bit, System.Boolean
                AsByte, tinyint, System.Byte
                AsCurrency, money, System.Decimal
                AsDate, date, System.DateTime
                AsDateTime, datetime, System.DateTime
                AsDateTimeOffset, datetimeoffset, System.DateTimeOffset
                AsDecimal, decimal, System.Decimal
                AsDouble, float, System.Double
                AsFixedLengthAnsiString, char, System.String
                AsFixedLengthString, nchar, System.String
                AsFloat, real, System.Single
                AsGuid, uniqueidentifier, System.Guid
                AsInt16, smallint, System.Int16
                AsInt32, int, System.Int32
                AsInt64, bigint, System.Int64
                AsString, nvarchar, System.String
                AsTime, time, System.TimeSpan
            */

            foreach (var c in table.Columns.Values)
            {
                switch (c.DbType)
                {
                    case "varchar": c.ClrType = typeof(System.String); break;
                    case "varbinary": c.ClrType = typeof(System.Byte[]); break;
                    case "bit": c.ClrType = typeof(System.Boolean); break;
                    case "tinyint": c.ClrType = typeof(System.Byte); break;
                    case "money": c.ClrType = typeof(System.Decimal); break;
                    case "date": c.ClrType = typeof(System.DateTime); break;
                    case "datetime": c.ClrType = typeof(System.DateTime); break;
                    case "datetimeoffset": c.ClrType = typeof(System.DateTimeOffset); break;
                    case "decimal": c.ClrType = typeof(System.Decimal); break;
                    case "float": c.ClrType = typeof(System.Double); break;
                    case "char": c.ClrType = typeof(System.String); break;
                    case "nchar": c.ClrType = typeof(System.String); break;
                    case "real": c.ClrType = typeof(System.Single); break;
                    case "uniqueidentifier": c.ClrType = typeof(System.Guid); break;
                    case "smallint": c.ClrType = typeof(System.Int16); break;
                    case "int": c.ClrType = typeof(System.Int32); break;
                    case "bigint": c.ClrType = typeof(System.Int64); break;
                    case "nvarchar": c.ClrType = typeof(System.String); break;
                    case "time": c.ClrType = typeof(System.TimeSpan); break;
                    default:
                        throw new ParalibException($"Db data type '{c.DbType}' not supported");

                }




            }            
        }

        protected virtual void FindPrimaryKeys(Table table)
        {
            string sql = "select";
            sql += " kcu.TABLE_NAME,kcu.COLUMN_NAME, kcu.ORDINAL_POSITION";
            sql += " from INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc";
            sql += " inner join";
            sql += " INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu";
            sql += " ON kcu.CONSTRAINT_CATALOG = tc.CONSTRAINT_CATALOG AND kcu.CONSTRAINT_SCHEMA = tc.CONSTRAINT_SCHEMA AND kcu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME";
            sql += $" where tc.CONSTRAINT_TYPE = 'PRIMARY KEY' and kcu.TABLE_NAME = '{table.Name}'";
            sql += " order by kcu.TABLE_NAME, kcu.ORDINAL_POSITION";


            var reader = ExecuteReader(sql);

            while (reader.Read())
            {
                table.Columns[reader.GetValue<string>("COLUMN_NAME")].IsPrimary = true;
            }

            reader.Close();


        }

        protected virtual void FindRelationships(Table table)
        {
            string sql = "SELECT";
            sql += " KCU1.CONSTRAINT_NAME AS 'FK_CONSTRAINT_NAME', KCU1.TABLE_NAME AS 'FK_TABLE_NAME', KCU1.COLUMN_NAME AS 'FK_COLUMN_NAME', KCU1.ORDINAL_POSITION AS 'FK_ORDINAL_POSITION',";
            sql += " KCU2.CONSTRAINT_NAME AS 'UQ_CONSTRAINT_NAME', KCU2.TABLE_NAME AS 'UQ_TABLE_NAME', KCU2.COLUMN_NAME AS 'UQ_COLUMN_NAME', KCU2.ORDINAL_POSITION AS 'UQ_ORDINAL_POSITION'";
            sql += " FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC";
            sql += " JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU1";
            sql += " ON KCU1.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG AND KCU1.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA AND KCU1.CONSTRAINT_NAME = RC.CONSTRAINT_NAME";
            sql += " JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU2";
            sql += " ON KCU2.CONSTRAINT_CATALOG = RC.UNIQUE_CONSTRAINT_CATALOG AND KCU2.CONSTRAINT_SCHEMA = RC.UNIQUE_CONSTRAINT_SCHEMA AND KCU2.CONSTRAINT_NAME = RC.UNIQUE_CONSTRAINT_NAME AND KCU2.ORDINAL_POSITION = KCU1.ORDINAL_POSITION";
            sql += $" WHERE KCU1.TABLE_NAME='{table.Name}' or KCU2.TABLE_NAME='{table.Name}'";

            List<Relationship> fkeys = new List<Relationship>();
            List<Relationship> refs = new List<Relationship>();

            var reader = ExecuteReader(sql);

            while (reader.Read())
            {
                string fkName = reader.GetValue<string>("FK_CONSTRAINT_NAME");
                string fkTable = reader.GetValue<string>("FK_TABLE_NAME");
                string fkColumn = reader.GetValue<string>("FK_COLUMN_NAME");
                string uqTable = reader.GetValue<string>("UQ_TABLE_NAME");
                string uqColumn = reader.GetValue<string>("UQ_COLUMN_NAME");

                if (fkTable==table.Name)
                {
                    fkeys.Add(new Relationship() { Name = fkName, OnTable = fkTable, OnColumn = fkColumn, OtherTable = uqTable, OtherColumn = uqColumn });

                    table.Columns[fkColumn].IsForeign= true;

                }

                if (uqTable == table.Name)
                {
                    refs.Add(new Relationship() { Name = fkName, OnTable = uqTable, OnColumn = uqColumn, OtherTable = fkTable, OtherColumn = fkColumn });
                }

            }

            table.ForeignKeys = fkeys.ToArray();
            table.References = refs.ToArray();

            reader.Close();


        }



        public virtual bool TableExists(string tableName)
        {
            throw new NotImplementedException();
        }


    }


}
