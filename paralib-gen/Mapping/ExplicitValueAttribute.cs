using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Mapping
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ExplicitValueAttribute : Attribute
    {
        public string Value { private set; get; }

        public ExplicitValueAttribute(string value)
        {
            Value = value;
        }

    }

}
