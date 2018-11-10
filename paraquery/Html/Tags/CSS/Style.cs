using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;

/*

    We reify CSS with classes and enums and IComplex values as mixed case
    properties, but we always allow for freeform property values with
    camel case properties.

    We don't reify in any way:
        shorthand properties
        initial
        inherit

    Camel-cased properties take precendent if both mixed and camel case 
    properties have values. That is, freeform properties (camel case strings)
    override strongly typed properties.

    StyleBase also has a Properties object that allows you to add addtional
    properties via an anonymous object. These properties override both
    freeform and typed properties.




    selector { property : value; }
                <------------->
                  declaration    

*/

namespace com.paraquery.Html
{
    public partial class Style : StyleBase, IDynamicValueContainer
    {
        public string color { get; set; }
        public Color? Color { get; set; }

        protected override string GetProperties()
        {
            //since stylebase doesn't add a final semicolon, do it here
            return $"{base.GetProperties()};";
        }

        bool IDynamicValueContainer.HasValue(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Background):
                    return _background != null;

                default:
                    throw new InvalidOperationException($"Property name {propertyName} not found");
            }

        }



    }
}
