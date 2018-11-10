using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;

namespace com.paraquery.Html
{
    /*

        Length is percentage.

        length can be <length> or <percentage>, <length> having units such as em, px, etc.


    */
    public class ColorStop: IComplexAttribute
    {
        public string color { set; get; }
        public Color? Color { set; get; }
        public string length { set; get; }
        public float? Length { set; get; }

        string IComplexAttribute.Value
        {
            get
            {
                string style = null;

                style = color ?? Color?.ToString();

                if (style != null)
                {
                    if (length!=null)
                    {
                        style = $"{style} {length}";
                    }
                    else
                    {
                        if (Length.HasValue)
                        {
                            style = $"{style} {Length.ToString()}%";
                        }
                    }
                }

                return style;
            }
        }
    }
}
