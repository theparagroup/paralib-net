﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;
using com.parahtml.Core;

namespace com.parahtml
{
    public class Percentage : LengthOrPercentage
    {
        public Percentage(float number)
        {
            _value = $"{number}%";
        }
    }
}
