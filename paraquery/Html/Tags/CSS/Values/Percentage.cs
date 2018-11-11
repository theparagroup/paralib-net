using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;

namespace com.paraquery.Html
{
    public class Percentage : ComplexValue
    {
        public Percentage(float number)
        {
            _value = $"{number}%";
        }
    }
}
