using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.NameValuePairs;

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

    public class PropertyDictionary : NVPDictionary
    {
        protected const char ValueContainerMarker = '!';

        public string[] ToDeclarations()
        {
            List<string> declarations = new List<string>();

            foreach (var property in this)
            {
                if (property.Key[0] != ValueContainerMarker)
                {
                    declarations.Add($"{PropertyBuilder.Customnate(property.Key)}: {property.Value}");
                }
                else
                {
                    var decls = property.Value.Split(';');

                    foreach (var decl in decls)
                    {
                        declarations.Add(decl.Trim());
                    }

                }
            }

            return declarations.ToArray();

        }
        public string ToDeclaration()
        {
            StringBuilder styleBuilder = new StringBuilder();
            KeyValuePair<string, string>? lastProperty = null;

            if (Count > 0)
            {
                foreach (var property in this)
                {
                    //after the first pass, pre-pend semicolon
                    if (lastProperty != null)
                    {
                        //but not if the last property was a value container
                        if (lastProperty.Value.Key[0]!= ValueContainerMarker)
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

            }

            return styleBuilder.ToString();

        }
       

    }
}





