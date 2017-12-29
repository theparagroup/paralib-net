using System;
using com.paralib.Data;
using System.Collections.Generic;

namespace com.paralib.Migrations.ParaTypes
{


    public class ColumnMetadata
    {
        public string Table { get; set; }
        public string Column { get; set; }
        public Type ClrType { get; set; }
        public ParaType ParaType { get; set; }
        public string Description { get; set; }
        public string Extended { get; set; }

        public static bool Creating { get; private set; }

        public static List<ColumnMetadata> Changes = new List<ColumnMetadata>();


        public static void CreateTable(FluentMigrator.Builders.Create.ICreateExpressionRoot create)
        {
            create.Table(Paralib.Dal.ColumnMetadataTable)
            .WithColumn("ID").AsInt64().PrimaryKey().Identity()
            .WithColumn("TABLE_NAME").AsString(256)
            .WithColumn("COLUMN_NAME").AsString(256)
            .WithColumn("PARA_TYPE").AsString(256).Nullable()
            .WithColumn("CLR_TYPE").AsString(256)
            .WithColumn("DESCRIPTION").AsString(256).Nullable()
            .WithColumn("EXTENDED").AsString(int.MaxValue).Nullable();

            create.UniqueConstraint().OnTable(Paralib.Dal.ColumnMetadataTable).Columns(new string[] { "TABLE_NAME", "COLUMN_NAME" });

            Creating = true;

        }

        public static void DeleteTable(FluentMigrator.Builders.Delete.IDeleteExpressionRoot delete)
        {
            delete.Table(Paralib.Dal.ColumnMetadataTable);
        }

        public static void Save(FluentMigrator.Migration migration)
        {
            foreach (ColumnMetadata cm in Changes)
            {
                migration.Delete.FromTable(Paralib.Dal.ColumnMetadataTable).Row(new { TABLE_NAME = cm.Table, COLUMN_NAME = cm.Column });
                migration.Insert.IntoTable(Paralib.Dal.ColumnMetadataTable).Row(new { TABLE_NAME = cm.Table, COLUMN_NAME = cm.Column, PARA_TYPE = cm.ParaType?.Name, CLR_TYPE = cm.ClrType.Name, DESCRIPTION = cm.Description, EXTENDED = cm.Extended });
            }

            //TODO delete tables that no longer exist?


            Changes.Clear();

        }


    }
}
