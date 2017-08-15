using System;


namespace com.paralib.Migrations.CodeGen.Conventions
{
    public class MicrosoftConvention : IConvention
    {

        public string GetClassName(string tableName, Pluralities plurality)
        {
            throw new NotImplementedException();
        }

        public string GetDisplayName(string columnName)
        {
            throw new NotImplementedException();
        }

        public string GetReferenceName(string columnName)
        {
            throw new NotImplementedException();
        }

        public string GetCollectionName(string tableName)
        {
            throw new NotImplementedException();
        }

        public string GetPropertyName(string columnName)
        {
            throw new NotImplementedException();
        }
    }
}
