using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using com.paralib.Gen.Mapping;

namespace com.parahtml.Core
{
    /*
        Contains a set of name value pairs that represents CSS properties.

        You can create CSS "custom properties" for use with the "var(property)"
        function by prefixing the member property name with an underscore:

            object._myProperty="foo";
            
            style="--my-property: foo;"

            something : var(--my-property)


    */

    public class PropertyBuilder:DictionaryBuilder<HtmlContext, PropertyDictionary>
    {
        protected new HtmlContext Context { private set; get; }

        //this needs to be the same as the Context option
        protected const char ValueContainerMarker = '!';

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


        public List<string> ToList(PropertyDictionary properties)
        {
            List<string> list = new List<string>();

            foreach (var property in properties)
            {
                if (property.Key[0] != ValueContainerMarker)
                {
                    list.Add($"{Customnate(property.Key)}: {property.Value}");
                }
                else
                {
                    var decls = property.Value.Split(';');

                    foreach (var decl in decls)
                    {
                        list.Add(decl.Trim());
                    }

                }
            }

            return list;

        }
        public string ToDeclaration(PropertyDictionary properties)
        {
            StringBuilder styleBuilder = new StringBuilder();
            KeyValuePair<string, string>? lastProperty = null;

            foreach (var property in properties)
            {
                //after the first pass, pre-pend semicolon
                if (lastProperty != null)
                {
                    //but not if the last property was a value container
                    if (lastProperty.Value.Key[0] != ValueContainerMarker)
                    {
                        styleBuilder.Append("; ");
                    }
                }

                if (property.Key[0] != ValueContainerMarker)
                {
                    styleBuilder.Append($"{PropertyBuilder.Customnate(property.Key)}: {property.Value}");
                }
                else
                {
                    styleBuilder.Append(property.Value);
                }

                lastProperty = property;
            }

            //there has to be a lastProperty since Count>0
            if (lastProperty.Value.Key[0] != ValueContainerMarker)
            {
                styleBuilder.Append(";");
            }

            return styleBuilder.ToString();
        }
    }
}
