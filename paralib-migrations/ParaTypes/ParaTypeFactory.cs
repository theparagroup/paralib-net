using System;
using com.paralib.Data;

namespace com.paralib.Migrations.ParaTypes
{
    public static class ParaTypeFactory
    {

        public static TNext AsParaType<TNext>(FluentMigrator.Builders.IColumnTypeSyntax<TNext> fluent, string paraTypeName, string description=null) where TNext : FluentMigrator.Infrastructure.IFluentSyntax
        {
            ParaType paraType = Paralib.ParaTypes[paraTypeName];

            ColumnMetadata cm = new ColumnMetadata() { ParaType=paraType, Description=description };

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
            else
            {
                throw new ParalibException($"ParaType \"{paraType.Type.Name}\" is not supported in migrations.");
            }
        }
    }
}
