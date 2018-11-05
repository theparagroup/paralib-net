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

        public HtmlComponent(HtmlContext context, Renderer starter) : base(context, starter)
        {
        }

        protected new HtmlContext Context
        {
            get
            {
                return (HtmlContext)base.Context;
            }
        }

        protected HtmlBuilder HtmlBuilder
        {
            get
            {
                return Context.HtmlBuilder;
            }
        }


        protected override void OnDebug(string text)
        {
            HtmlRenderer.Comment(Writer, text);
        }

    }
}
