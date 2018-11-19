using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;

namespace com.parahtml.Attributes
{
    /*
        Scripts can be inline or external.

        Script tags may appear any number of times in the head or body. 

        Scripts execute in order, one at a time (unless defer or async).
        An external script will not execute until the one before it completes.

        Scripts can only access parts of the DOM that exist when the parser
        hit the script tag.

    */

    public class ExternalScriptAttributes : GlobalAttributes
    {
        public MediaType Type { set; get; } = new MediaType(MediaTypes.Application.JavaScript);
        public string type { set; get; }

        //external scripts only
        public Url Src { set; get; }
        public string src { set; get; }

        public string charset { set; get; }

        public string async { set; get; }

        public bool? Defer { set; get; }
        public string defer { set; get; }

    }
}
