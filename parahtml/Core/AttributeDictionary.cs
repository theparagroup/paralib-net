using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.NameValuePairs;

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

    public class AttributeDictionary : NVPDictionary
    {

       
       

        public string[] ToAttributes(bool minimizeBooleans, bool escapeAttributeValues)
        {
            List<string> attributes = new List<string>();

            foreach (var attribute in this)
            {
                var name = attribute.Key;
                var value = attribute.Value;

                if (name != null)
                {
                    if (value == "true" && !minimizeBooleans)
                    {
                        value = name;
                    }

                    if (value == "true")
                    {
                        //boolean style attributes (e.g. "readonly")
                        attributes.Add($"{name}");
                    }
                    else
                    {
                        if (escapeAttributeValues)
                        {
                            value = value.Replace("\"", "&quot;");
                        }

                        attributes.Add($"{name}=\"{value}\"");
                    }

                }



            }

            return attributes.ToArray();
        }

        public string ToAttributesString(bool minimizeBooleans, bool escapeAttributeValues)
        {
            StringBuilder attributeBuilder = new StringBuilder();
            bool firstPass = true;

            var attributes = ToAttributes(minimizeBooleans, escapeAttributeValues);

            foreach (var attribute in attributes)
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





