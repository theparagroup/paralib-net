using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags.Attributes;

namespace com.paraquery.Html.Tags
{
    /*

         Contains a set of name value pairs that represents attributes on a tag.

         Null values indicate true boolean values, and should be rendered according to the MinimizeBooleans option.


        Attribute Names
            http://www.w3.org/TR/2000/REC-xml-20001006#NT-Name

            First character         => letter | _ | :
            Additional characters   => letter | digit | underscore | colon | period | dash, or a “CombiningChar” or “Extender” character, which I believe allows Unicode attributes names.


     */

    public class AttributeDictionary : NVPDictionary
    {

        public static AttributeDictionary Attributes(object attributes)
        {
            AttributeDictionary dictionary = new AttributeDictionary();
            DictionaryBuilder.Build(dictionary, attributes, false);
            return dictionary;
        }

        public static AttributeDictionary Attributes<T>(Action<T> attributes = null, object additional = null) where T : GlobalAttributes, new()
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

                    DictionaryBuilder.Build(dictionary, a, typeof(T), false);
                }

                //merge any additional anonymous object-based attributes
                if (additional != null)
                {
                    DictionaryBuilder.Build(dictionary, additional, false);
                }

                //return null if there are no attributes
                if (dictionary.Count > 0)
                {
                    return dictionary;
                }
            }

            return null;
        }

    }
}





