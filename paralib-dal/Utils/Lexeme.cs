using System;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Text.RegularExpressions;

namespace com.paralib.Dal.Utils
{
    /*
    
        fixing stupid microsoft ideas about the english language

        status/statuses

    */


    public static class Lexeme
    {
        private static PluralizationService _pluralizer=PluralizationService.CreateService(new CultureInfo("en-US"));

        private static bool IsPluralOverride(ref bool plural, string value, string pluralPatternm, string singularPattern)
        {
            if (Regex.IsMatch(value, pluralPatternm, RegexOptions.IgnoreCase))
            {
                plural = true;
                return true;
            }
            else if (Regex.IsMatch(value, singularPattern, RegexOptions.IgnoreCase))
            {
                plural = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsOverride(ref string value, string pattern, MatchEvaluator evaluator)
        {
            if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
            {
                value = Regex.Replace(value, pattern, evaluator, RegexOptions.IgnoreCase);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string Pluralize(string value)
        {
            if (IsOverride(ref value, "status$", m => m.Value + "es")) return value;

            string result = _pluralizer.Pluralize(value);
            return result;
        }

        public static bool IsPlural(string value)
        {
            bool plural= _pluralizer.IsPlural(value);

            IsPluralOverride(ref plural, value, "statuses$", "status$");

            return plural;
        }

        public static string Singularize(string value)
        {
            if (IsOverride(ref value, "statuses$", m => m.Value.Substring(0, 6))) return value;

            string result = _pluralizer.Singularize(value);
            return result;
        }

        public static bool IsSingular(string value)
        {
            bool singular = _pluralizer.IsSingular(value);

            if (IsPluralOverride(ref singular, value, "statuses$", "status$")) return !singular;

            return singular;

        }


    }
}
