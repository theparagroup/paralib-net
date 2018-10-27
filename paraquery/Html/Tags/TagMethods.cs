using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;
using com.paraquery.Rendering;

namespace com.paraquery.Html.Tags
{

    public partial class TagBuilder
    {

        // ******************************************************************* boiler plate signatures - these could be code generated


        /*  -------------------------------- DIV -------------------------------------- */

        public virtual Renderer Div(object additional = null)
        {
            return Div(null, additional);
        }

        public virtual Renderer Div(Action<GlobalAttributes> attributes, object additional = null)
        {
            return new BlockTag(this, "div", Attributes(attributes, additional));
        }

        /*  -------------------------------- SPAN -------------------------------------- */

        public virtual Renderer Span(object additional = null)
        {
            return Span(null, additional);
        }

        public virtual Renderer Span(Action<GlobalAttributes> attributes, object additional = null)
        {
            return new InlineTag(this, "span", Attributes(attributes, additional));
        }

        /*  -------------------------------- HR -------------------------------------- */

        public virtual Renderer Hr(object additional = null)
        {
            return Hr(null, additional);
        }

        public virtual Renderer Hr(Action<HrAttributes> attributes, object additional = null)
        {
            return new BlockTag(this, "hr", Attributes(attributes, additional), true);
        }

        /*  -------------------------------- SCRIPT -------------------------------------- */

        public virtual Renderer Script(object additional = null)
        {
            return Script(null, additional);
        }

        public virtual Renderer Script(Action<ScriptAttributes> attributes, object additional = null)
        {
            return new BlockTag(this, "script", Attributes(attributes, new { additional, defaults = new { type = "application/javascript" } }));
        }


    }
}
