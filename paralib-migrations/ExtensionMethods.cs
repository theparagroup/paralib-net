﻿using System;
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

        public static TNext AsParaType<TNext>(this FluentMigrator.Builders.IColumnTypeSyntax<TNext> fluent, string name) where TNext : FluentMigrator.Infrastructure.IFluentSyntax
        {
            return ParaTypes.ParaTypeFactory.AsParaType<TNext>(fluent, name);
        }

        public static void StandardLogTable(this FluentMigrator.Builders.Create.ICreateExpressionRoot create, string tableName = "log")
        {
            Logging.StandardLog.CreateStandardLogTable(create, tableName);
        }

        public static void StandardLogTable(this FluentMigrator.Builders.Delete.IDeleteExpressionRoot delete, string tableName = "log")
        {
            Logging.StandardLog.DeleteStandardLogTable(delete, tableName);
        }


    }
}