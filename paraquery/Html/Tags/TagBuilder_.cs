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

        public virtual Renderer Block(string name, Action<GlobalAttributes> attributes = null, bool empty=false)
        {
            return new Tag(this, name, true, empty, Attributes(attributes));
        }

        public virtual Renderer Inline(string name, Action<GlobalAttributes> attributes = null, bool empty = false)
        {
            return new Tag(this, name, false, empty, Attributes(attributes));
        }

        public virtual Renderer Div(Action<GlobalAttributes> attributes=null)
        {
            return new Tag(this, "div", true, false, Attributes(attributes));
        }

        public virtual Renderer Span(Action<GlobalAttributes> attributes=null)
        {
            return new Tag(this, "span", false, false, Attributes(attributes));
        }

        public virtual Renderer Hr(Action<HrAttributes> attributes = null)
        {
            return new Tag(this, "hr", true, true, Attributes(attributes));
        }

        public virtual Renderer Script(Action<ScriptAttributes> attributes = null)
        {
            return new Tag(this, "script", true, false, Attributes(attributes));
        }


    }
}
