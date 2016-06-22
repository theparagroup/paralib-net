using System;
using NET = System.ComponentModel.DataAnnotations;

namespace com.paralib.DataAnnotations
{
    
    public class StringAttribute:NET.ValidationAttribute
    {
        protected int _length;

        public StringAttribute(int length):base("The memeber {0} can't be longer than {1}")
        {
            _length = length;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, _length);
        }

        protected override NET.ValidationResult IsValid(object value, NET.ValidationContext validationContext)
        {

            //we want to pass null values (use Required) and throw if value is not a string
            if (value!= null && ((string)value).Length>_length)
            {
                return new NET.ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            }

            return NET.ValidationResult.Success;
        }


    }
}
