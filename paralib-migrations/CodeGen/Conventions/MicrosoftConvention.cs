using System;
using com.paralib.Dal.Metadata;


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

        public string GetPropertyName(string columnName)
        {
            throw new NotImplementedException();
        }
    }
}
