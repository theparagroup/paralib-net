using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;
using com.paraquery.Rendering;

namespace com.paraquery.Html
{
    public abstract class HtmlComponent : Component
    {
        public HtmlComponent(TagBuilder tagBuilder, RenderModes renderModes, bool visible) : base(tagBuilder, renderModes, visible)
        {
        }

        protected override void Comment(string text)
        {
            HtmlRenderer.HtmlComment(_writer, text);
        }

    }
}
