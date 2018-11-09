using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Tags.Attributes
{
    public class BodyAttributes : GlobalAttributes
    {
        public Url Background { get; set; }

        public Color? ALink { get; set; }
        public Color? BGColor { get; set; }
        public Color? Link { get; set; }
        public Color? Text { get; set; }
        public Color? VLink { get; set; }
    }
}
