using System;
using System.Linq;
using System.Collections.Generic;
using com.paralib.Dal.Metadata;
using com.paralib.Dal.Utils;

namespace com.paralib.Migrations.CodeGen
{
    /*

        Abstract base class for all generators:

            ModelGenerator
            LogicGenerator
            MetadataGenerator
            EfGenerator
            etc.

        
        Support various writers, options, and conventions.


    */

    public abstract class Generator
    {
        protected Dictionary<string, Table> _tables;
        protected IClassWriter _writer;
        protected IConvention Convention { get; private set; }
        protected ClassOptions ClassOptions { get; private set; }
        
        public Generator(IClassWriter writer, IConvention convention, Dictionary<string, Table> tables, ClassOptions classOptions)
        {
            _tables = tables;
            _writer = writer;
            Convention = convention;
            ClassOptions = classOptions;
        }

        protected virtual Properties GetProperties(string tableName, string columnName)
        {
            Table table = _tables[tableName];
            Column column = table.Columns[columnName];
            return column.Properties;
        }

        protected virtual Dictionary<string,string> GetExtendedProperties(Properties properties)
        {
            if (properties?.Extended != null)
            {
                string extendedJson = properties.Extended;
                var extended = Utils.Json.DeSerialize<Dictionary<string, string>>(extendedJson);

                return extended;
            }
            else
            {
                return null;
            }
        }

        protected virtual Dictionary<string, string> GetExtendedProperties(string tableName, string columnName)
        {
            return GetExtendedProperties(GetProperties(tableName, columnName));
        }

        protected virtual string GetExtendedProperty(Properties properties, string extendedPropertyName)
        {
            var extended = GetExtendedProperties(properties);

            if (extended?.ContainsKey(extendedPropertyName)??false)
            {
                return extended[extendedPropertyName];
            }
            else
            {
                return null;
            }
        }

        protected virtual string GetExtendedProperty(string tableName, string columnName, string extendedPropertyName)
        {
            return GetExtendedProperty(GetProperties(tableName, columnName), extendedPropertyName);
        }


        protected virtual string GetClassName(string tableName)
        {
            return Convention.GetClassName(tableName, Pluralities.Singular);
        }

        protected bool IsNullable(Column column)
        {
            if (column.IsNullable && CSharpTypes.HasNullable(column.ClrType)) return true;
            return false;
        }

        protected void Start(string className)
        {
            _writer.Start(className);
        }

        protected void Write(string text)
        {
            _writer.Write(text);
        }

        protected void WriteLine(string text=null)
        {
            _writer.WriteLine(text);
        }

        protected void End()
        {
            _writer.End();
        }

    }
}
