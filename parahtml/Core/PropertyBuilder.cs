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
        protected const char ValueContainerMarker = '!';

        public PropertyBuilder(HtmlContext context) : base(context)
        {
            Context = context;
        }

        protected override string OnValueContainer(HtmlContext context, string name)
        {
            return $"{ValueContainerMarker}{name}";
        }

        public static string Customnate(string name)
        {
            if (name?[0] == '_')
            {
                return $"--{name.Remove(0, 1)}";
            }

            return name;
        }

        protected override string OnName(HtmlContext context, string prefix, string name)
        {
            if (!string.IsNullOrEmpty(prefix) && !string.IsNullOrEmpty(name))
            {
                char[] p = prefix.ToCharArray();
                char[] n = name.ToCharArray();

                if (char.IsLower(n[0]))
                {
                    p[0] = char.ToLower(p[0]);
                    n[0] = char.ToUpper(n[0]);
                }

                return $"{new string(p)}{new string(n)}";
            }
            else
            {
                return $"{prefix}{name}";
            }
        }

        protected PropertyDictionary Flatten(PropertyDictionary dictionary)
        {
            return Merge(dictionary, f => char.IsUpper(f[0]), s => !char.IsUpper(s[0]), k => HtmlBuilder.HyphenateMixedCase(k).ToLower());
        }

        public PropertyDictionary Properties(object properties)
        {
            PropertyDictionary dictionary = new PropertyDictionary();
            Build(dictionary, properties);
            return Flatten(dictionary);
        }

        public List<string> ToList(PropertyDictionary properties)
        {
            List<string> list = new List<string>();

            foreach (var property in properties)
            {
                list.Add($"{Customnate(property.Key)}: {property.Value}");
            }

            return list;
        }

        public string ToDeclaration(PropertyDictionary properties)
        {
            StringBuilder propertyBuilder = new StringBuilder();
            bool firstPass = true;

            var list = ToList(properties);

            foreach (var property in list)
            {
                if (firstPass)
                {
                    firstPass = false;
                }
                else
                {
                    propertyBuilder.Append("; ");
                }

                propertyBuilder.Append(property);
            }

            return propertyBuilder.ToString();

        }

    }
}
