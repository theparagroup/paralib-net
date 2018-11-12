﻿using System;
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
    public class ColorStop: IComplexValue
    {
        public string color { set; get; }
        public Color? Color { set; get; }
        public string length { set; get; }
        public Length Length { set; get; }
        public string percentage { set; get; }
        public Percentage Percentage { set; get; }

        public string ToValue(Context context)
        {
            string value = null;

            value = color ?? PropertyBuilder.Lowernate(Color);

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
