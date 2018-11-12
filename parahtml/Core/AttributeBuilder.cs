using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen;
using com.paralib.Gen.Mapping;
using com.parahtml.Attributes;

namespace com.parahtml.Core
{
    /*

        Contains a set of name value pairs that represents attributes on a tag.

        Null values indicate true boolean values, and should be rendered according to the MinimizeBooleans option.


       Attribute Names
           http://www.w3.org/TR/2000/REC-xml-20001006#NT-Name

           First character         => letter | _ | :
           Additional characters   => letter | digit | underscore | colon | period | dash, or a “CombiningChar” or “Extender” character, which I believe allows Unicode attributes names.


    */

    public class AttributeBuilder:DictionaryBuilder<HtmlContext, AttributeDictionary>
    {
        protected new HtmlContext Context { private set; get; }

        public AttributeBuilder(HtmlContext context):base(context)
        {
            Context = context;
        }

        protected override void OnAdd(HtmlContext context, AttributeDictionary dictionary, string name, string value)
        {
            //special rules
            if (value != null)
            {
                //merge classes
                if (name == "class" && dictionary.ContainsKey("class"))
                {
                    //note Union() will throw if one of the arrays is null
                    //we know value isn't null, but if you have a property Class=true,
                    //the what's in the dictionary could be
                    string[] oldclasses = dictionary["class"]?.Split(' ') ?? new string[] { };
                    string[] newClasses = value.Split(' ');

                    //this removes duplicates
                    string[] classes = oldclasses.Union(newClasses).ToArray();

                    value = string.Join(" ", classes);
                }
            }


            base.OnAdd(context, dictionary, name, value);
        }

        public AttributeDictionary Attributes(object attributes)
        {
            AttributeDictionary dictionary = new AttributeDictionary();
            Build(dictionary, attributes, false);
            return dictionary;
        }

        public AttributeDictionary Attributes<T>(Action<T> attributes = null, object additional = null) where T : GlobalAttributes, new()
        {
            //let's keep it simple if there is nothing to do
            if (attributes != null || additional != null)
            {
                //create a new dictionary to hold the name value pairs
                AttributeDictionary dictionary = new AttributeDictionary();

                //execute init action and build, if exists
                if (attributes != null)
                {
                    T a = new T();

                    attributes(a);

                    Build(dictionary, a, typeof(T), false);
                }

                //merge any additional anonymous object-based attributes
                if (additional != null)
                {
                    Build(dictionary, additional, false);
                }

                //return null if there are no attributes
                if (dictionary.Count > 0)
                {
                    return dictionary;
                }
            }

            return null;
        }

        public List<string> ToList(AttributeDictionary attributes)
        {
            List<string> list = new List<string>();

            foreach (var attribute in attributes)
            {
                var name = attribute.Key;
                var value = attribute.Value;

                if (name != null)
                {
                    if (value == "true" && !Context.Options.MinimizeBooleans)
                    {
                        value = name;
                    }

                    if (value == "true")
                    {
                        //boolean style attributes (e.g. "readonly")
                        list.Add($"{name}");
                    }
                    else
                    {
                        if (Context.Options.EscapeAttributeValues)
                        {
                            value = value.Replace("\"", "&quot;");
                        }

                        list.Add($"{name}=\"{value}\"");
                    }

                }



            }

            return list;
        }

        public string ToTagContents(AttributeDictionary attributes)
        {
            //no leading or trailing spaces

            StringBuilder attributeBuilder = new StringBuilder();
            bool firstPass = true;

            var list = ToList(attributes);

            foreach (var attribute in list)
            {
                if (firstPass)
                {
                    firstPass = false;
                }
                else
                {
                    attributeBuilder.Append(" ");
                }

                attributeBuilder.Append(attribute);
            }

            return attributeBuilder.ToString();

        }
    }
}
