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

        public static TNext AsParaString<TNext>(this FluentMigrator.Builders.IColumnTypeSyntax<TNext> fluent, int maximumLength, string description = null, int? minimumLength = null, string regEx = null, string tooLongErrorMessage = null, string tooShortErrorMessage = null, string badFormatErrorMessage = null) where TNext : FluentMigrator.Infrastructure.IFluentSyntax
        {
            return ParaTypes.ParaTypeFactory.AsParaString<TNext>(fluent, maximumLength, description, minimumLength, regEx, tooLongErrorMessage, tooShortErrorMessage, badFormatErrorMessage);
        }

        public static void StandardLogTable(this FluentMigrator.Builders.Create.ICreateExpressionRoot create, string tableName = Logging.StandardLog.DefaultTableName)
        {
            Logging.StandardLog.CreateTable(create, tableName);
        }

        public static void StandardLogTable(this FluentMigrator.Builders.Delete.IDeleteExpressionRoot delete, string tableName = Logging.StandardLog.DefaultTableName)
        {
            Logging.StandardLog.DeleteTable(delete, tableName);
        }

        public static void ColumnMetadataTable(this FluentMigrator.Builders.Create.ICreateExpressionRoot create)
        {
            ParaTypes.ColumnMetadata.CreateTable(create);
        }

        public static void ColumnMetadataTable(this FluentMigrator.Builders.Delete.IDeleteExpressionRoot delete)
        {
            ParaTypes.ColumnMetadata.DeleteTable(delete);
        }



    }
}
