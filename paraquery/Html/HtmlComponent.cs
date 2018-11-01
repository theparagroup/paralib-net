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
        protected TagBuilder TagBuilder { private set; get; }

        public HtmlComponent(TagBuilder tagBuilder, RenderModes renderModes, bool visible) : base(tagBuilder.Context, renderModes, visible)
        {
            TagBuilder = tagBuilder;
        }

        protected override void OnDebug(string text)
        {
            HtmlRenderer.Comment(Writer, text);
        }

    }
}
