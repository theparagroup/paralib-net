using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Mapping
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class PrefixAttribute : Attribute
    {
        public string Prefix { private set; get; }

        public PrefixAttribute(string prefix=null)
        {
            Prefix = prefix;
        }

    }

}
