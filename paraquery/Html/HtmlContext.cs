using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html
{
    /*

        A version of Context that has HTML-centric functionality.


    */
    public class HtmlContext : Context
    {
        public HtmlBuilder HtmlBuilder { private set; get; }


        public HtmlContext(Writer writer, Action<Options> options = null) : base(writer, options)
        {
            HtmlBuilder = new HtmlBuilder(this);
        }
    }
}
