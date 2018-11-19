using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected override string OnValueContainer(HtmlContext context, string name)
        {
            return $"{ValueContainerMarker}{name}";
        }


        public PropertyBuilder(HtmlContext context):base(context)
        {
            Context = context;
        }

      

        public static string Customnate(string name)
        {
            if (name?[0] == '_')
            {
                return $"--{name.Remove(0, 1)}";
            }

            return name;
        }


        public PropertyDictionary Properties(object properties)
        {
            //note: we call this in case-sensitive mode so we can hyphenate mixed case
            var dictionary = new PropertyDictionary();
            Build(dictionary, properties);

            //merge mixed and camel case duplicates
            var merged = new PropertyDictionary();

            //add mixed cased first
            foreach (var key in dictionary.Keys)
            {
                if (char.IsUpper(key[0]))
                {
                    merged.Add(HtmlBuilder.HyphenateMixedCase(key).ToLower(), dictionary[key]);
                }
            }

            //overwrite with camel if it exists (attributedictionary will replace it for us)
            foreach (var key in dictionary.Keys)
            {
                //we say !IsUpper instead of IsLower because attribute names can start with
                //underscore and other non-alpha characters... we want these too!
                if (!char.IsUpper(key[0]))
                {
                    merged[HtmlBuilder.HyphenateMixedCase(key).ToLower()] = dictionary[key];
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
