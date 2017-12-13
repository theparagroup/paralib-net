using System;
using System.Text.RegularExpressions;

namespace com.paralib.Data
{
    public class StringType : ParaType
    {
        public int? MinimumLength { get; internal set; }
        public int MaximumLength { get; protected set; }
        public string RegEx { get; internal set; }

        private static readonly string _tooLongErrorMessage = "'{0}' must be {1} characters or shorter";
        public string TooLongErrorMessage {get;set;}

        private static readonly string _tooShortErrorMessage = "'{0}' must be {1} characters or longer";
        public string TooShortErrorMessage { get; set; }

        private static readonly string _badFormatErrorMessage = "'{0}' is invalid";
        public string BadFormatErrorMessage { get; set; }

        public StringType(string name, int maximumLength) : base(name, typeof(string))
        {
            MaximumLength = maximumLength;
        }

        public override string Validate(string displayName, object value)
        {

            //we want to pass null values (use [Required] for that) and throw if value is not a string

            if (value != null && ((string)value).Length > MaximumLength)
            {
                return string.Format(TooLongErrorMessage ?? _tooLongErrorMessage, displayName, MaximumLength);
            }

            if (MinimumLength.HasValue)
            {
                if (value != null && ((string)value).Length < MinimumLength)
                {
                    return string.Format(TooShortErrorMessage ?? _tooShortErrorMessage, displayName, MinimumLength);
                }
            }

            if (RegEx != null)
            {
                if (value != null && !Regex.IsMatch((string)value, RegEx))
                {
                    return string.Format(BadFormatErrorMessage ?? _badFormatErrorMessage, displayName);
                }
            }

            return null;
        }


    }



}
