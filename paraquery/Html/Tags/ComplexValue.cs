using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Tags
{
    /*

        This is handy for complex values where all the values can be set in 
        the constructor.

    */
    public class ComplexValue:IComplexValue
    {
        protected string _value;

        public string ToValue()
        {
            return _value;
        }

    }
}
