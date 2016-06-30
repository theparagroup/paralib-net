using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Migrations
{
    public static class ExtensionMethods
    {
        //public static TNext AsString<TNext>(this FluentMigrator.Builders.IColumnTypeSyntax<TNext> fluent, string name) where TNext : FluentMigrator.Infrastructure.IFluentSyntax
        //{
        //    return AsParaType(fluent, name);
        //}

        public static TNext AsParaType<TNext>(this FluentMigrator.Builders.IColumnTypeSyntax<TNext> fluent, string name, string description=null) where TNext : FluentMigrator.Infrastructure.IFluentSyntax
        {
            return ParaTypes.ParaTypeFactory.AsParaType<TNext>(fluent, name, description);
        }

        public static void StandardLogTable(this FluentMigrator.Builders.Create.ICreateExpressionRoot create, string tableName = Logging.StandardLog.DefaultTableName)
        {
            Logging.StandardLog.CreateTable(create, tableName);
        }

        public static void StandardLogTable(this FluentMigrator.Builders.Delete.IDeleteExpressionRoot delete, string tableName = Logging.StandardLog.DefaultTableName)
        {
            Logging.StandardLog.DeleteTable(delete, tableName);
        }

        public static void ColumnMetadataTable(this FluentMigrator.Builders.Create.ICreateExpressionRoot create, string tableName = ParaTypes.ColumnMetadata.DefaultTableName)
        {
            ParaTypes.ColumnMetadata.CreateTable(create, tableName);
        }

        public static void ColumnMetadataTable(this FluentMigrator.Builders.Delete.IDeleteExpressionRoot delete, string tableName = ParaTypes.ColumnMetadata.DefaultTableName)
        {
            ParaTypes.ColumnMetadata.DeleteTable(delete, tableName);
        }



    }
}
