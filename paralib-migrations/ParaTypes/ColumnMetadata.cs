using System;
using com.paralib.Data;
using System.Collections.Generic;

namespace com.paralib.Migrations.ParaTypes
{


    public class ColumnMetadata
    {
        public string Table { get; set; }
        public string Column { get; set; }
        public ParaType ParaType { get; set; }
        public string Description { get; set; }

        public static bool Creating { get; private set; }

        public const string DefaultTableName= "paralib_column_metadata";

        public static List<ColumnMetadata> Changes = new List<ColumnMetadata>();



        public static void CreateTable(FluentMigrator.Builders.Create.ICreateExpressionRoot create, string tableName = DefaultTableName)
        {
            create.Table(tableName)
            .WithColumn("id").AsInt32().PrimaryKey().Identity()
            .WithColumn("table").AsString(256)
            .WithColumn("column").AsString(256)
            .WithColumn("para_type").AsString(256)
            .WithColumn("description").AsString(256).Nullable();

            create.UniqueConstraint().OnTable(tableName).Columns(new string[] { "table", "column" });

            Creating = true;

        }

        public static void DeleteTable(FluentMigrator.Builders.Delete.IDeleteExpressionRoot delete, string tableName = DefaultTableName)
        {
            delete.Table(tableName);
        }

        public static void Save(FluentMigrator.Migration migration, string tableName = DefaultTableName)
        {
            foreach (ColumnMetadata cm in Changes)
            {
                migration.Delete.FromTable(tableName).Row(new { table = cm.Table, column = cm.Column });
                migration.Insert.IntoTable(tableName).Row(new { table = cm.Table, column = cm.Column, para_type = cm.ParaType.Name, description = cm.Description });
            }

            Changes.Clear();

        }


    }
}
