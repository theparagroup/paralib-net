using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace com.paralib.Mvc.Tables
{


    public class TableDefinition
    {
        public Type Type { get; protected set; }
        public IEnumerable Source { get; private set; }
        public string TableId { get; set; }
        public string TableClass { get; set; }
        public string TrClass { get; set; }
        public string TrHeaderClass { get; set; }
        public string TrRowClass { get; set; }
        public string ThClass { get; set; }
        public string ThHeaderClass { get; set; }
        public string TdClass { get; set; }
        public string TdRowClass { get; set; }

        public TableDefinition(IEnumerable source, Type type)
        {
            Source = source;
            Type = type;
        }

        public Column[] Columns
        {
            get
            {

                if (Type == null)
                {
                    throw new InvalidOperationException("Type can't be null. Did you pass in an empty collection?");
                }


                PropertyInfo[] propertyInfos;
                propertyInfos = Type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                List<Column> columnNames = new List<Column>();

                foreach (var propertyInfo in propertyInfos)
                {
                    ColumnAttribute columnAttribute = propertyInfo.GetCustomAttributes(true).FirstOrDefault() as ColumnAttribute;

                    if (columnAttribute != null)
                    {
                        if (columnAttribute.Hide) continue;
                    }

                    Column column = new Column();
                    column.Name = propertyInfo.Name;
                    column.Attribute = columnAttribute;

                    columnNames.Add(column);
                }

                return columnNames.ToArray();
            }

        }

        public string GetValue(object row, Column column)
        {
            return Type.GetProperty(column.Name).GetValue(row, null)?.ToString();
        }




    }






}


