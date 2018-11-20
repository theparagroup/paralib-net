using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Mapping
{
    /*

        This is handy for complex values where all the values can be set in 
        the constructor.

    */
    public class ComplexValue<C>:IComplexValue<C> where C:Context
    {
        protected string _value;

        protected virtual string Value
        {
            get
            {
                return _value;
            }
        }

        public virtual string ToValue(C context)
        {
            return Value;
        }

        public override string ToString()
        {
            return Value;
        }

    }
}
