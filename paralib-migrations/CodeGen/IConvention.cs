﻿using System;

namespace com.paralib.Migrations.CodeGen
{
    public interface IConvention
    {
        string GetClassName(string tableName, Pluralities plurality);
        string GetPropertyName(string columnName);
        string GetEntityName(string columnName);
        string GetDisplayName(string columnName);
        string Implements { get; set; }
        string Ctor { get; set; }
        string EfPrefix { get; }
    }
}
