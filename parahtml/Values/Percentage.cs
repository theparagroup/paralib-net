using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;

namespace com.parahtml
{
    public class Percentage : ComplexValue
    {
        public Percentage(float number)
        {
            _value = $"{number}%";
        }
    }
}
