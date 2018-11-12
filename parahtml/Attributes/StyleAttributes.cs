using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml.Attributes
{
    public class StyleAttributes : GlobalAttributes
    {
        public string media { set; get; }
        public string type { set; get; } = "text/css";

    }
}
