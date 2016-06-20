using System;
using FluentMigrator;

namespace com.paralib.Migrations.Logging
{
    public class StandardLogging
    {

        public static void Down(Migration migration, string tableName="log")
        {
            migration
            .Delete.Table(tableName);
        }


        public static void Up(Migration migration, string tableName = "log")
        {
            migration
            .Create.Table(tableName)
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


    }
}
