using System;


namespace com.paralib.Migrations.CodeGen.Conventions
{
    public class MicrosoftConvention : IConvention
    {
        public string Ctor { get; set; } 

        public string Implements { get; set; }

        public string EfPrefix { get; } = null;

        public string GetClassName(string tableName, bool singularize)
        {
            throw new NotImplementedException();
        }

        public string GetDisplayName(string columnName)
        {
            throw new NotImplementedException();
        }

        public string GetPropertyName(string columnName, bool keyify=true)
        {
            throw new NotImplementedException();
        }
    }
}
