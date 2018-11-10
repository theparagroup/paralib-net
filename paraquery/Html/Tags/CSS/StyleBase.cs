using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;
using System.Text.RegularExpressions;

namespace com.paraquery.Html
{
    public abstract class StyleBase : IComplexAttribute
    {
        public object Properties { get; set; }

        protected static string Hyphenate(string name)
        {
            //mixed case should be replaced with hyphens (but not first letter)
            return Regex.Replace(name, @"([a-z])([A-Z])", "$1-$2");
        }

        protected static string Spacenate(string name)
        {
            //mixed case should be replaced with spaces (but not first letter)
            return Regex.Replace(name, @"([a-z])([A-Z])", "$1 $2");
        }

        protected virtual string GetProperties()
        {

            //note: we call this in case-sensitive mode so we can hyphenate mixed case
            var dictionary = new AttributeDictionary();
            AttributeBuilder.BuildAttributeDictionary(dictionary, this, true);

            //merge mixed and camel case duplicates
            var properties = new AttributeDictionary();

            //add mixed cased first
            foreach (var key in dictionary.Keys)
            {
                if (char.IsUpper(key[0]))
                {
                    properties.Add(Hyphenate(key).ToLower(), dictionary[key]);
                }
            }

            //overwrite with camel if it exists (attributedictionary will replace it for us)
            foreach (var key in dictionary.Keys)
            {
                //we say !IsUpper instead of IsLower because attribute names can start with
                //underscore and other non-alpha characters... we want these too!
                if (!char.IsUpper(key[0]))
                {
                    properties[Hyphenate(key).ToLower()] =dictionary[key];
                }
            }

            string style = null;

            if (properties.Count > 0)
            {
                //doesn't add a semicolon to the end
                foreach (var property in properties)
                {
                    if (style!=null)
                    {
                        style += "; ";
                    }

                    if (property.Key[0]!='!')
                    {
                        style += $"{property.Key}: {property.Value}";
                    }
                    else
                    {
                        style += property.Value;
                    }

                }


                //style = string.Join("; ", properties.Select(e => $"{e.Key}: {e.Value}").ToArray());
            }

            return style;

        }

        string IComplexAttribute.Value
        {
            get
            {
                return GetProperties();
            }
        }

    }
}
