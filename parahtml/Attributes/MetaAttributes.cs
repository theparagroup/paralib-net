using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;

namespace com.parahtml.Attributes
{
    public class MetaAttributes : GlobalAttributes
    {
        public string name { set; get; }
        public string content { set; get; }
    }
}
