using System;
using com.paralib.Data;

namespace com.paralib.Migrations.ParaTypes
{
    public static class ParaTypeFactory
    {
        private static void RecordMetadata<TNext>(FluentMigrator.Builders.IColumnTypeSyntax<TNext> fluent, ParaType paraType, string description, string extended) where TNext : FluentMigrator.Infrastructure.IFluentSyntax
        {
            ColumnMetadata cm = new ColumnMetadata() { ParaType = paraType, Description = description, Extended=extended };

            //record metadata
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

        public static TNext AsParaType<TNext>(FluentMigrator.Builders.IColumnTypeSyntax<TNext> fluent, string paraTypeName, string description=null) where TNext : FluentMigrator.Infrastructure.IFluentSyntax
        {
            ParaType paraType = Paralib.ParaTypes[paraTypeName];

            RecordMetadata<TNext>(fluent, paraType, description, null);

            return Decode<TNext>(fluent, paraType);
        }

        public static TNext AsParaString<TNext>(FluentMigrator.Builders.IColumnTypeSyntax<TNext> fluent, int maximumLength, string description = null, int? minimumLength = null, string regEx = null,  string tooShortMsg = null, string tooLongMsg = null, string formatMsg = null) where TNext : FluentMigrator.Infrastructure.IFluentSyntax
        {
            ParaType paraType = new StringType(DataAnnotations.ParaTypes.ParaString, maximumLength);

            //[ParaString(0, 5, regEx:"foo", tooShortMsg:"Too short!", tooLongMsg:"too long {0}", formatMsg:"bad")]

            string extended = $"{minimumLength??0}, {maximumLength}";
            
            if (regEx!=null)
            {
                extended += $", regEx:\"{regEx}\"";
            }

            if (tooShortMsg != null)
            {
                extended += $", tooShortMsg:\"{tooShortMsg}\"";
            }

            if (tooLongMsg != null)
            {
                extended += $", tooLongMsg:\"{tooLongMsg}\"";
            }

            if (formatMsg != null)
            {
                extended += $", formatMsg:\"{formatMsg}\"";
            }

            extended = $"[ParaString({extended})]";            

            RecordMetadata<TNext>(fluent, paraType, description, extended);

            return Decode<TNext>(fluent, paraType);
        }
    }
}
