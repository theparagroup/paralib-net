using System;
using FluentMigrator;
using com.paralib.Migrations.ParaTypes;

namespace com.paralib.Migrations
{
    public abstract class ParaMigration:Migration
    {
        public string ColumnMetadataTableName { get; set; }

        protected string GetColumnMetadataTableName()
        {
            return ColumnMetadataTableName ?? ColumnMetadata.DefaultTableName;
        }

        public abstract void OnUp();

        public abstract void OnDown();

        public override void Down()
        {
            OnDown();

            if (Schema.Table(GetColumnMetadataTableName()).Exists())
            {
                ColumnMetadata.Save(this);
            }
        }

        public override void Up()
        {
            OnUp();

            if (Schema.Table(GetColumnMetadataTableName()).Exists() || ColumnMetadata.Creating)
            {
                ColumnMetadata.Save(this);
            }
        }



    }
}
