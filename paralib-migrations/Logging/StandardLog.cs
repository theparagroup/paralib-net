using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Migrations.Logging
{
    public static class StandardLog
    {
        public const string DefaultTableName= "log";

        public static void CreateTable(FluentMigrator.Builders.Create.ICreateExpressionRoot create, string tableName = DefaultTableName)
        {
            //see ParaAdoNetAppender for how this table is used

            create.Table(tableName)
            .WithColumn("id").AsInt64().PrimaryKey().NotNullable().Identity()
            .WithColumn("date").AsDateTime().NotNullable()
            .WithColumn("timestamp").AsInt32().NotNullable()
            .WithColumn("thread").AsString(32).NotNullable()
            .WithColumn("tid").AsInt32().NotNullable()
            .WithColumn("level").AsString(16).NotNullable()
            .WithColumn("logger").AsString(256).NotNullable()
            .WithColumn("method").AsString(256).NotNullable()
            .WithColumn("user").AsString(256).NotNullable()
            .WithColumn("message").AsString(4000).NotNullable();
        }

        public static void DeleteTable(FluentMigrator.Builders.Delete.IDeleteExpressionRoot delete, string tableName = DefaultTableName)
        {
            delete.Table(tableName);
        }

    }
}
