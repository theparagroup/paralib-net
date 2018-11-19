using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml.Attributes
{
    public class StyleAttributes : GlobalAttributes
    {
        //TODO but very complicated
        public string media { set; get; }

        public MediaType Type { set; get; } = new MediaType(MediaTypes.Text.Css);
        public string type { set; get; } = "text/css";

    }
}
