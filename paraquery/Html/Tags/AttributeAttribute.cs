using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Tags
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class AttributeAttribute : Attribute
    {
        public string Name { private set; get; }
        public string Value { set; get; }

        public AttributeAttribute(string name)
        {
            Name = name;
        }

    }

}
