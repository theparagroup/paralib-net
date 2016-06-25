using System;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace com.paralib.Dal.Utils
{
    public static class Lexeme
    {
        private static PluralizationService _pluralizer=PluralizationService.CreateService(new CultureInfo("en-US"));

        public static string Pluralize(string value)
        {
            return _pluralizer.Pluralize(value);
        }

        public static bool IsPlural(string value)
        {
            return _pluralizer.IsPlural(value);
        }

        public static string Singularize(string value)
        {
            return _pluralizer.Singularize(value);
        }

        public static bool IsSingular(string value)
        {
            return _pluralizer.IsSingular(value);
        }


    }
}
