using System;

namespace com.paralib.Migrations.CodeGen
{
    public interface IConvention
    {

        //employee_types -> EmployeeType
        //employee_types -> EmployeeTypes
        string GetClassName(string tableName, Pluralities plurality);

        //employee_type_id -> EmployeeTypeId
        string GetPropertyName(string columnName);

        //employee_type_id -> EmployeeType
        string GetReferenceName(string columnName);

        //user_types -> IList<EfUserType> UserTypes;
        string GetCollectionName(string tableName);

        //employee_type_id -> "Employee Type Id"
        string GetDisplayName(string columnName);


    }
}
