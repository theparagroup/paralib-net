using System;

namespace com.paralib.Migrations.CodeGen
{
    public interface IConvention
    {
        string GetClassName(string tableName, bool singularize);
        string GetPropertyName(string columnName, bool keyify=true);
        string GetDisplayName(string columnName);
        string Implements { get; set; }
        string Ctor { get; set; }
        string EfPrefix { get; }
    }
}
