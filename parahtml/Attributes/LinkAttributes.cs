using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;

namespace com.parahtml.Attributes
{
    public class LinkAttributes : GlobalAttributes
    {
        public Url Href { set; get; }
        public string href { set; get; }

        public string rel { set; get; }

        public MediaType Type { set; get; } = new MediaType(MediaTypes.Application.JavaScript);
        public string type { set; get; }

    }
}
