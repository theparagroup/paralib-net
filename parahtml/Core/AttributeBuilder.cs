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
    public class AttributeBuilder:DictionaryBuilder
    {
        protected new HtmlContext Context { private set; get; }

        public AttributeBuilder(HtmlContext context):base(context)
        {
            Context = context;
        }

        protected override void OnAdd(Context context, NVPDictionary dictionary, string name, string value)
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

        /*
    


                           
*/
    }
}
