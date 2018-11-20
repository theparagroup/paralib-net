using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;

namespace com.parahtml.Attributes
{
    public class BodyAttributes : GlobalAttributes
    {
        public Url Background { set; get; }
        public string background { set; get; }

        public Color? ALink { set; get; }
        public string alink { set; get; }

        public Color? BGColor { set; get; }
        public string bgcolor { set; get; }

        public Color? Link { set; get; }
        public string link { set; get; }

        public Color? Text { set; get; }
        public string text { set; get; }

        public Color? VLink { set; get; }
        public string vlink { set; get; }
    }
}
