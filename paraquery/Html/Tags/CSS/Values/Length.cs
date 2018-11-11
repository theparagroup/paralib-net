﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;

namespace com.paraquery.Html
{
    public class Length : ComplexValue
    {
        private Length(float number, string unit)
        {
            _value = $"{number}{unit}";
        }

        public Length(float number, LengthUnits unit) : this(number, PropertyDictionary.Lowernate(unit))
        {
        }

        public Length(float number, ViewPortLengthUnits unit) : this(number, PropertyDictionary.Lowernate(unit))
        {
        }

        public Length(float number, FontLengthUnits unit) : this(number, PropertyDictionary.Lowernate(unit))
        {
        }

    }
}
