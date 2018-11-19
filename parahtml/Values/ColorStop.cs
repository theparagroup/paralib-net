using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;
using com.parahtml.Core;

namespace com.parahtml
{
    /*

        Length is percentage.

        length can be <length> or <percentage>, <length> having units such as em, px, etc.


    */
    public class ColorStop: IComplexValue<HtmlContext>
    {
        public string color { set; get; }
        public Color? Color { set; get; }
        public string length { set; get; }
        public Length Length { set; get; }
        public string percentage { set; get; }
        public Percentage Percentage { set; get; }

        public string ToValue(HtmlContext context)
        {
            string value = null;

            value = color ?? HtmlBuilder.StructToValue(Color);

            if (value != null)
            {
                if (length != null)
                {
                    value = $"{value} {length}";
                }
                else
                {
                    if (Length!=null)
                    {
                        value = $"{value} {Length.ToValue(context)}";
                    }
                }
            }

            return value;
        }
    }
}
