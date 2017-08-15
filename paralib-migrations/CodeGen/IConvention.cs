using System;
using com.paralib.Dal.Metadata;

namespace com.paralib.Migrations.CodeGen
{
    public interface IConvention
    {

        //employee_types -> EmployeeType
        //employee_types -> EmployeeTypes
        string GetClassName(string tableName, Pluralities plurality);

        //employee_type_id -> EmployeeTypeId
        string GetPropertyName(string columnName);

        //employee_type_id -> "Employee Type Id"
        string GetDisplayName(string columnName);


    }
}
