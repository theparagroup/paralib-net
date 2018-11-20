using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Mapping
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class NullValueAttribute : Attribute
    {
        public object Value { private set; get; }

        public NullValueAttribute(object value)
        {
            Value = value;
        }

    }

}
