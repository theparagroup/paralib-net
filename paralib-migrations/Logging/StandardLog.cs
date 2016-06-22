using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Migrations.Logging
{
    public static class StandardLog
    {
        public static void CreateStandardLogTable(FluentMigrator.Builders.Create.ICreateExpressionRoot create, string tableName = "log")
        {
            create.Table(tableName)
            .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
            .WithColumn("date").AsDateTime().NotNullable()
            .WithColumn("timestamp").AsInt32().NotNullable()
            .WithColumn("thread").AsString(32).NotNullable()
            .WithColumn("tid").AsInt32().NotNullable()
            .WithColumn("level").AsString(16).NotNullable()
            .WithColumn("logger").AsString(256).NotNullable()
            .WithColumn("method").AsString(256).NotNullable()
            .WithColumn("user").AsString(256).NotNullable()
            .WithColumn("message").AsString(256).NotNullable();
        }

        public static void DeleteStandardLogTable(FluentMigrator.Builders.Delete.IDeleteExpressionRoot delete, string tableName = "log")
        {
            delete.Table(tableName);
        }

    }
}
