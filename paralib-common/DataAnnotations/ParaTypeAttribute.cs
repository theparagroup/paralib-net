using System;
using NET = System.ComponentModel.DataAnnotations;
using com.paralib.Data;

namespace com.paralib.DataAnnotations
{
    
    public class ParaTypeAttribute:NET.ValidationAttribute
    {
        protected ParaType _paraType;

        public ParaTypeAttribute(string typeName)
        {
            _paraType = Paralib.ParaTypes[typeName];
        }


        protected override NET.ValidationResult IsValid(object value, NET.ValidationContext validationContext)
        {
            string error = _paraType.Validate(validationContext.DisplayName, value);

            if (error!=null)
            {
                return new NET.ValidationResult(error,new string[] {validationContext.MemberName });
            }

            return NET.ValidationResult.Success;
        }


    }
}
