using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;
using com.paraquery.Rendering;

namespace com.paraquery.Html.Tags
{
    /*

            list of empty elements

            <area />
            <base />
            <br />
            <col />
            <command />
            <embed />
            <hr />
            <img />
            <input />
            <keygen />
            <link />
            <menuitem />
            <meta />
            <param />
            <source />
            <track />
            <wbr />

        */

    public partial class TagBuilder
    {

        // ******************************************************************* boiler plate signatures - these could be code generated


        /*  -------------------------------- DIV -------------------------------------- */

        public virtual Element Div(object additional = null)
        {
            return Div(null, additional);
        }

        public virtual Element Div(Action<GlobalAttributes> attributes, object additional = null)
        {
            return new Element(this, RendererTypes.Block, "div", Attributes(attributes, additional));
        }

        /*  -------------------------------- SPAN -------------------------------------- */

        public virtual Element Span(object additional = null)
        {
            return Span(null, additional);
        }

        public virtual Element Span(Action<GlobalAttributes> attributes, object additional = null)
        {
            return new Element(this, RendererTypes.Inline, "span", Attributes(attributes, additional));
        }

        /*  -------------------------------- HR -------------------------------------- */

        public virtual Element Hr(object additional = null)
        {
            return Hr(null, additional);
        }

        public virtual Element Hr(Action<HrAttributes> attributes, object additional = null)
        {
            return new Element(this, RendererTypes.Block, "hr", Attributes(attributes, additional), true);
        }

        /*  -------------------------------- SCRIPT -------------------------------------- */

        public virtual Element Script(object additional = null)
        {
            return Script(null, additional);
        }

        public virtual Element Script(Action<ScriptAttributes> attributes, object additional = null)
        {
            return new Element(this, RendererTypes.Block, "script", Attributes(attributes, new { additional, defaults = new { type = "application/javascript" } }));
        }


    }
}
