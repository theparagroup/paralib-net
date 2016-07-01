using System;


namespace com.paralib.Migrations.CodeGen.Conventions
{
    public class Microsoft : IConvention
    {
        public string Ctor { get; set; } 

        public string Implements { get; set; } 

        public string GetClassName(string tableName)
        {
            throw new NotImplementedException();
        }

        public string GetDisplayName(string columnName)
        {
            throw new NotImplementedException();
        }

        public string GetPropertyName(string columnName)
        {
            throw new NotImplementedException();
        }
    }
}
