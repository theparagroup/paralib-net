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

        public virtual Renderer Div(Action<GlobalAttributes> attributes=null)
        {
            return new BlockTag(this, "div", Attributes(attributes));
        }

        public virtual Renderer Span(Action<GlobalAttributes> attributes=null)
        {
            return new InlineTag(this, "span", Attributes(attributes));
        }

        public virtual Renderer Hr(Action<HrAttributes> attributes = null)
        {
            return new BlockTag(this, "hr", Attributes(attributes), true);
        }

        public virtual Renderer Script(Action<ScriptAttributes> attributes = null)
        {
            return new BlockTag(this, "script", Attributes(attributes));
        }


    }
}
