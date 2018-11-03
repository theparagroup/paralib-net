using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;
using com.paraquery.Html.Tags;

namespace com.paraquery.Html
{
    public class HtmlContext : Context
    {
        public TagBuilder TagBuilder { private set; get; }


        public HtmlContext(Writer writer, Action<Options> init = null) : base(writer, init)
        {
            TagBuilder = new TagBuilder(this);
        }
    }
}
