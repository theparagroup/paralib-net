using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml.Attributes
{
    public class ImgAttributes : GlobalAttributes
    {
        public Url Src { set; get; }
        public string src { set; get; }

        public string alt { set; get; }

        public int? Border { set; get; }
        public string border { set; get; }

        public int? Height { set; get; }
        public string height { set; get; }

        public int? Width { set; get; }
        public string width { set; get; }

        public int? HSpace { set; get; }
        public string hspace { set; get; }

        public int? VSpace { set; get; }
        public string vspace { set; get; }


    }
}
