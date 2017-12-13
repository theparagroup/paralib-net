using System;
using NET = System.ComponentModel.DataAnnotations;
using com.paralib.Data;

namespace com.paralib.DataAnnotations
{
    
    public class ParaTypeAttribute:NET.ValidationAttribute
    {
        protected ParaType _paraType;

        protected ParaTypeAttribute()
        {

        }

        public ParaTypeAttribute(string typeName)
        {
            if (typeName==ParaTypes.ParaString)
            {
                throw new Exception("Use [ParaString] attribute instead of [ParaType(ParaTypes.ParaString)]");
            }

            _paraType = Paralib.ParaTypes[typeName];
        }

        public ParaType ParaType
        {
            get
            {
                return _paraType;
            }
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
