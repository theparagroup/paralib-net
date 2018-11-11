using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Tags.Attributes
{
    public class StyleAttributes : GlobalAttributes
    {
        public string media { set; get; }
        public string type { set; get; } = "text/css";

    }
}
