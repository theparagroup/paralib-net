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

    public class ScriptAttributes : GlobalAttributes
    {
        public string type { set; get; }
        public MediaType Type { set; get; } = new MediaType(MediaTypes.Application.JavaScript);


        [ExplicitName("xml:space")]
        public string xmlspace { get; set; }

        [ExplicitName("xml:space")]
        [ExplicitValue("preserve")]
        public bool? XmlSpace { get; set; }


    }
}
