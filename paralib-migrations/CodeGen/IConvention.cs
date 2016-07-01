using System;

namespace com.paralib.Migrations.CodeGen
{
    public interface IConvention
    {
        string GetClassName(string tableName);
        string GetPropertyName(string columnName);
        string GetDisplayName(string columnName);
        string Implements { get; set; }
        string Ctor { get; set; }
    }
}
