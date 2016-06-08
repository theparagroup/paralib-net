using System;
using FluentMigrator;

namespace com.paralib.migrations
{
    public class Logging
    {

        public static void Down(Migration migration)
        {
            migration
            .Delete.Table("log");
        }


        public static void Up(Migration migration)
        {
            migration
            .Create.Table("log")
            .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
            .WithColumn("date").AsDateTime().NotNullable()
            .WithColumn("level").AsString(50).NotNullable()
            .WithColumn("logger").AsString(255).NotNullable()
            .WithColumn("method").AsString(255).NotNullable()
            .WithColumn("message").AsString(4000).NotNullable();
        }


    }
}
