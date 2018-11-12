using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace com.paraquery.Html.Tags
{
    public class PropertyBuilder:DictionaryBuilder
    {
        protected new HtmlContext Context { private set; get; }

        public PropertyBuilder(HtmlContext context):base(context)
        {
            Context = context;
        }

        public static string Hyphenate(string name)
        {
            //mixed case should be replaced with hyphens (but not first letter)
            return Regex.Replace(name, @"([a-z])([A-Z])", "$1-$2");
        }

        public static string Spacenate(string name)
        {
            //mixed case should be replaced with spaces (but not first letter)
            return Regex.Replace(name, @"([a-z])([A-Z])", "$1 $2");
        }

        public static string Customnate(string name)
        {
            if (name?[0] == '_')
            {
                return $"--{name.Remove(0, 1)}";
            }

            return name;
        }

        public static string Lowernate<T>(T value) where T : struct
        {
            return value.ToString().ToLower();
        }

        public static string Lowernate<T>(T? value) where T : struct
        {
            if (value.HasValue)
            {
                return value.ToString().ToLower();
            }
            else
            {
                return null;
            }
        }

        public PropertyDictionary Properties(object properties)
        {
            //note: we call this in case-sensitive mode so we can hyphenate mixed case
            var dictionary = new PropertyDictionary();
            Build(dictionary, properties, true);

            //merge mixed and camel case duplicates
            var merged = new PropertyDictionary();

            //add mixed cased first
            foreach (var key in dictionary.Keys)
            {
                if (char.IsUpper(key[0]))
                {
                    merged.Add(Hyphenate(key).ToLower(), dictionary[key]);
                }
            }

            //overwrite with camel if it exists (attributedictionary will replace it for us)
            foreach (var key in dictionary.Keys)
            {
                //we say !IsUpper instead of IsLower because attribute names can start with
                //underscore and other non-alpha characters... we want these too!
                if (!char.IsUpper(key[0]))
                {
                    merged[Hyphenate(key).ToLower()] = dictionary[key];
                }
            }

            return merged;
        }

       
    }
}
