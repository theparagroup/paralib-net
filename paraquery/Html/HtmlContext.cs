using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html
{
    public class HtmlContext : Context
    {
        public HtmlBuilder HtmlBuilder { private set; get; }


        public HtmlContext(Writer writer, Action<Options> init = null) : base(writer, init)
        {
            HtmlBuilder = new HtmlBuilder(this);
        }
    }
}
