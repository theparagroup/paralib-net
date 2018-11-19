using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;
using com.parahtml.Core;

namespace com.parahtml
{
    public class Length : ComplexValue
    {
        private Length(float number, string unit)
        {
            _value = $"{number}{unit}";
        }

        public Length(float number, LengthUnits unit) : this(number, HtmlBuilder.StructToValue(unit))
        {
        }

        public Length(float number, ViewPortLengthUnits unit) : this(number, HtmlBuilder.StructToValue(unit))
        {
        }

        public Length(float number, FontLengthUnits unit) : this(number, HtmlBuilder.StructToValue(unit))
        {
        }

    }
}
