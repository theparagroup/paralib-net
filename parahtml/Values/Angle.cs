﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;
using com.parahtml.Core;

namespace com.parahtml
{
    public class Angle:ComplexValue<HtmlContext>
    {
        public Angle(float number, AngleUnits unit = AngleUnits.Deg)
        {
            _value= $"{number}{unit.ToString().ToLower()}";
        }
    }
}
