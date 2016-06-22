using System;
using System.Collections.Generic;
using NET = System.ComponentModel.DataAnnotations;

namespace com.paralib.DataAnnotations
{
    public static class ObjectValidator
    {
        public static List<ValidationError> Validate(object obj)
        {
            var context = new NET.ValidationContext(obj, serviceProvider: null, items: null);
            var results = new List<NET.ValidationResult>();
            var valid = NET.Validator.TryValidateObject(obj, context, results, true);

            List<ValidationError> retval = null;

            if (!valid)
            {
                retval = new List<ValidationError>();

                foreach (var vr in results)
                {
                    string memberName = null;

                    foreach (var mn in vr.MemberNames)
                    {
                        memberName = mn;
                        break;
                    }

                    retval.Add(new ValidationError() { MemberName = memberName, ErrorMessage = vr.ErrorMessage });
                }

            }

            return retval;
        }
    }
}
