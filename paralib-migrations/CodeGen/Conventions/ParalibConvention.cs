using com.paralib.Dal.Utils;
using System;
using System.Text.RegularExpressions;

namespace com.paralib.Migrations.CodeGen.Conventions
{
    public class ParalibConvention : IConvention
    {
        public string Ctor { get; set; } 

        public string Implements { get; set; }

        public string EfPrefix { get; } = "Ef";

        public string GetClassName(string tableName, bool singularize)
        {
            //employee_types -> EmployeeType
            var result = Regex.Replace(tableName, "^.", m => m.Value.ToUpper());
            result = Regex.Replace(result, "_.", m => m.Value.ToUpper());

            string[] parts = result.Split('_');

            if (singularize) parts[parts.Length - 1] = Lexeme.Singularize(parts[parts.Length - 1]);

            result = string.Join("",parts);

            return result;
        }

        public string GetPropertyName(string columnName)
        {
            //employee_type_id -> EmployeeTypeId
            var result = Regex.Replace(columnName, "^.", m => m.Value.ToUpper());
            result = Regex.Replace(result, "_.", m => new string(new char[] { char.ToUpper(m.Value[1]) }));
            return result;
        }

        public string GetDisplayName(string columnName)
        {
            return Regex.Replace(GetPropertyName(columnName), "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }

    }
}
