using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Mvc.Tables
{

    [AttributeUsage(validOn: AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        public bool Hide { get; set; } = false;
        public bool Raw { get; set; } = false;
        public string Description { get; set; }
        public string Url { get; set; }
        public string Param0 { get; set; }
        public string Param1 { get; set; }
        public string ThClass { get; set; }
        public string TdClass { get; set; }
    }

}
