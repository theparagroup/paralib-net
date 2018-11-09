using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Tags.Attributes
{
    /*
        Scripts can be inline or external.

        Script tags may appear any number of times in the head or body. 

        Scripts execute in order, one at a time (unless defer or async).
        An external script will not execute until the one before it completes.

        Scripts can only access parts of the DOM that exist when the parser
        hit the script tag.

    */

    public class ScriptAttributes : GlobalAttributes
    {
        public string Charset { get; set; }
        public string Type { get; set; }

        [Attribute("xml:space", Value = "preserve")]
        public bool? XmlSpace { get; set; }

        //external scripts only
        public Url Src { get; set; }
        public string Async { get; set; }
        public bool? Defer { get; set; }

    }
}
