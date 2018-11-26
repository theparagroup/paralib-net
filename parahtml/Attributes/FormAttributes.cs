using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml.Attributes
{
    public class FormAttributes : GlobalAttributes
    {
        public Url Action { set; get; }
        public string action { set; get; }

        public Methods Method { set; get; }
        public string method { set; get; }
    }
}
