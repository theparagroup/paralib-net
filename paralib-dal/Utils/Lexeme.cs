using System;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Text.RegularExpressions;

namespace com.paralib.Dal.Utils
{
    /*
    
        fixing stupid microsoft ideas about the english language

        status/statuses
        info/info
        course/courses

        //TODO this should be simpler and easy for developers to extend via config files.

    */


    public static class Lexeme
    {
        private static PluralizationService _pluralizer=PluralizationService.CreateService(new CultureInfo("en-US"));

        private static bool IsPluralOverride(ref bool plural, string value, string pluralPattern, string singularPattern)
        {
            if (Regex.IsMatch(value, pluralPattern, RegexOptions.IgnoreCase))
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
            if (IsOverride(ref value, "info$", m => m.Value)) return value;
            if (IsOverride(ref value, "course$", m => m.Value + "s")) return value;
            if (IsOverride(ref value, "log$", m => m.Value)) return value;

            string result = _pluralizer.Pluralize(value);
            return result;
        }

        public static bool IsPlural(string value)
        {
            bool plural= _pluralizer.IsPlural(value);

            IsPluralOverride(ref plural, value, "statuses$", "status$");
            IsPluralOverride(ref plural, value, "info$", "info$");
            IsPluralOverride(ref plural, value, "courses$", "course$");
            IsPluralOverride(ref plural, value, "log$", "log$");

            return plural;
        }

        public static string Singularize(string value)
        {
            if (IsOverride(ref value, "statuses$", m => m.Value.Substring(0, 6))) return value;
            if (IsOverride(ref value, "info$", m => m.Value)) return value;
            if (IsOverride(ref value, "courses$", m => m.Value.Substring(0, 6))) return value;
            if (IsOverride(ref value, "log$", m => m.Value)) return value;

            string result = _pluralizer.Singularize(value);
            return result;
        }

        public static bool IsSingular(string value)
        {
            bool singular = _pluralizer.IsSingular(value);

            if (IsPluralOverride(ref singular, value, "statuses$", "status$")) return !singular;
            if (IsPluralOverride(ref singular, value, "info$", "info$")) return !singular;
            if (IsPluralOverride(ref singular, value, "courses$", "course$")) return !singular;
            if (IsPluralOverride(ref singular, value, "log$", "log$")) return !singular;

            return singular;

        }


    }
}
