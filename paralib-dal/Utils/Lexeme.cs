using System;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Text.RegularExpressions;

namespace com.paralib.Dal.Utils
{
    public static class Lexeme
    {
        private static PluralizationService _pluralizer=PluralizationService.CreateService(new CultureInfo("en-US"));

        public static string Pluralize(string value)
        {
            string result = _pluralizer.Pluralize(value);

            //fixing stupid microsoft ideas about the english language
            //status->statuses
            result = Regex.Replace(result, "status$", m => m.Value + "es", RegexOptions.IgnoreCase);

            return result;
        }

        public static bool IsPlural(string value)
        {
            return _pluralizer.IsPlural(value);
        }

        public static string Singularize(string value)
        {
            string result = _pluralizer.Singularize(value);

            //fixing stupid microsoft ideas about the english language
            //statuses->status
            result = Regex.Replace(result, "statuses$", m => m.Value.Substring(0,6),RegexOptions.IgnoreCase);

            return result;
        }

        public static bool IsSingular(string value)
        {
            return _pluralizer.IsSingular(value);
        }


    }
}
