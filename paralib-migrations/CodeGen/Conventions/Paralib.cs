using System;
using System.Text.RegularExpressions;

namespace com.paralib.Migrations.CodeGen.Conventions
{
    public class Paralib : IConvention
    {
        public string Ctor { get; set; } 

        public string Implements { get; set; } 

        public string GetClassName(string tableName)
        {
            return tableName;
        }

        public string GetPropertyName(string columnName)
        {
            return columnName;
        }

        public string GetDisplayName(string columnName)
        {
            return Regex.Replace(GetPropertyName(columnName), "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }

    }
}
