using System;
using NET = System.ComponentModel.DataAnnotations;
using com.paralib.Data;

namespace com.paralib.DataAnnotations
{
    
    public class ParaStringAttribute: ParaTypeAttribute
    {

        public ParaStringAttribute(int minimumLength, int maximumLength, string regEx=null, string tooLongMsg=null, string tooShortMsg=null, string formatMsg=null)
        {
            var paraType= new Data.StringType(ParaTypes.ParaString, maximumLength);
            paraType.MinimumLength = minimumLength == 0 ? null: (int?)minimumLength; 
            paraType.RegEx = regEx;
            paraType.TooLongErrorMessage = tooLongMsg;
            paraType.TooShortErrorMessage = tooShortMsg;
            paraType.BadFormatErrorMessage = formatMsg;

            _paraType = paraType;

        }

    }
}
