using System;
using System.Collections.Generic;
using com.paralib.Utils;
using com.paralib.Data;

namespace com.paralib.Migrations.ParaTypes
{
    public static class ParaTypeFactory
    {
        public static Dictionary<string,string> GetValuesAsDictionary(object extended)
        {
            //could be null, dictionary or anon type
            var dictionary = extended as Dictionary<string, string>;

            if (dictionary == null)
            {
                //value must be null or non-dictionary type

                if (extended != null)
                {

                    //copy property value pairs to dictionary
                    var properties = extended.GetType().GetProperties();

                    if (properties.Length>0)
                    {
                        //must be non-dict type with some values
                        dictionary = new Dictionary<string, string>();

                        foreach (var property in properties)
                        {
                            dictionary.Add(property.Name, property.GetValue(extended)?.ToString());
                        }

                    }
                }

            }

            //returns null if no values
            return dictionary;
        }

        public static void RecordMetadata<TNext>(FluentMigrator.Builders.IColumnTypeSyntax<TNext> fluent, ParaType paraType, string description, object extended) where TNext : FluentMigrator.Infrastructure.IFluentSyntax
        {
            RecordMetadata<TNext>(fluent, paraType, paraType.Type, description, extended);
        }

        public static void RecordMetadata<TNext>(FluentMigrator.Builders.IColumnTypeSyntax<TNext> fluent, Type clrType, string description, object extended) where TNext : FluentMigrator.Infrastructure.IFluentSyntax
        {
            RecordMetadata<TNext>(fluent, null, clrType, description, extended);
        }

        private static void RecordMetadata<TNext>(FluentMigrator.Builders.IColumnTypeSyntax<TNext> fluent, ParaType paraType, Type clrType, string description, object extended) where TNext : FluentMigrator.Infrastructure.IFluentSyntax
        {
            //serialize extended metadata
            string extendedJson = null;

            var dictionary = GetValuesAsDictionary(extended);

            if (dictionary != null)
            {
                //we want to serialize a dictionary here
                extendedJson = Json.Serialize(dictionary);
            }


            //record metadata
            ColumnMetadata cm = new ColumnMetadata() { ClrType = clrType, ParaType = paraType, Description = description, Extended = extendedJson };

            if (fluent is FluentMigrator.Builders.Create.Table.CreateTableExpressionBuilder)
            {
                var tableBuilder = (FluentMigrator.Builders.Create.Table.CreateTableExpressionBuilder)fluent;

                cm.Table = tableBuilder.Expression.TableName;
                cm.Column = tableBuilder.CurrentColumn.Name;
            }
            else if (fluent is FluentMigrator.Builders.Alter.Column.AlterColumnExpressionBuilder)
            {
                var alterBuilder = (FluentMigrator.Builders.Alter.Column.AlterColumnExpressionBuilder)fluent;

                cm.Table = alterBuilder.Expression.TableName;
                cm.Column = alterBuilder.Expression.Column.Name;
            }
            else if (fluent is FluentMigrator.Builders.Alter.Table.AlterTableExpressionBuilder)
            {
                var alterBuilder = (FluentMigrator.Builders.Alter.Table.AlterTableExpressionBuilder)fluent;

                cm.Table = alterBuilder.Expression.TableName;
                cm.Column = alterBuilder.CurrentColumn.Name;
            }
            else
            {
                throw new ParalibException($"Builder type {fluent.GetType().Name} not supported.");
            }


            ColumnMetadata.Changes.Add(cm);

        }

        private static TNext Decode<TNext>(FluentMigrator.Builders.IColumnTypeSyntax<TNext> fluent, ParaType paraType) where TNext : FluentMigrator.Infrastructure.IFluentSyntax
        {
            if (paraType.Type == typeof(string))
            {
                return fluent.AsString(((StringType)paraType).MaximumLength);
            }
            else if (paraType.Type == typeof(int))
            {
                return fluent.AsInt32();
            }
            else if (paraType.Type == typeof(long))
            {
                return fluent.AsInt64();
            }
            else if (paraType.Type == typeof(byte[]))
            {
                return fluent.AsBinary();
            }
            else if (paraType.Type == typeof(DateTime))
            {
                return fluent.AsDateTime();
            }
            else if (paraType.Type == typeof(TimeSpan))
            {
                return fluent.AsTime();
            }
            else if (paraType.Type == typeof(decimal))
            {
                return fluent.AsDecimal();
            }
            else if (paraType.Type == typeof(Boolean))
            {
                return fluent.AsBoolean();
            }
            else if (paraType.Type == typeof(Guid))
            {
                return fluent.AsGuid();
            }
            else if (paraType.Type == typeof(float))
            {
                return fluent.AsFloat();
            }
            else if (paraType.Type == typeof(double))
            {
                return fluent.AsDouble();
            }
            else
            {
                throw new ParalibException($"ParaType \"{paraType.Type.Name}\" is not supported in migrations.");
            }

        }

        public static TNext AsParaType<TNext>(FluentMigrator.Builders.IColumnTypeSyntax<TNext> fluent, string paraTypeName, string description=null, object values=null) where TNext : FluentMigrator.Infrastructure.IFluentSyntax
        {
            ParaType paraType = Paralib.ParaTypes[paraTypeName];

            RecordMetadata<TNext>(fluent, paraType, description, values);

            return Decode<TNext>(fluent, paraType);
        }

        public static TNext AsParaString<TNext>(FluentMigrator.Builders.IColumnTypeSyntax<TNext> fluent, int maximumLength, string description = null, int? minimumLength = null, string regEx = null,  string tooShortMsg = null, string tooLongMsg = null, string formatMsg = null, object values =null) where TNext : FluentMigrator.Infrastructure.IFluentSyntax
        {
            ParaType paraType = new StringType(DataAnnotations.ParaTypes.ParaString, maximumLength);

            //[ParaString(0, 5, regEx:"foo", tooShortMsg:"Too short!", tooLongMsg:"too long {0}", formatMsg:"bad")]

            string attribute = $"{minimumLength??0}, {maximumLength}";
            
            if (regEx!=null)
            {
                attribute += $", regEx:\"{regEx}\"";
            }

            if (tooShortMsg != null)
            {
                attribute += $", tooShortMsg:\"{tooShortMsg}\"";
            }

            if (tooLongMsg != null)
            {
                attribute += $", tooLongMsg:\"{tooLongMsg}\"";
            }

            if (formatMsg != null)
            {
                attribute += $", formatMsg:\"{formatMsg}\"";
            }

            if (values==null)
            {
                values = new Dictionary<string, string>();
            }

            //add parastring attribute to any existing attributes
            var dictionary = GetValuesAsDictionary(values)??new Dictionary<string, string>();

            dictionary.Add(nameof(ExtendedProperties.ParatypeAttribute), $"[ParaString({attribute})]");

            RecordMetadata<TNext>(fluent, paraType, description, values);

            return Decode<TNext>(fluent, paraType);
        }
    }
}
