using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Attributes
{
    public class ScriptAttributes : GlobalAttributes
    {
        public string Async { get; set; }
        public string Charset { get; set; }
        public string Defer { get; set; }
        public string Src { get; set; }
        public string Type { get; set; } = "application/javascript";
    }
}
