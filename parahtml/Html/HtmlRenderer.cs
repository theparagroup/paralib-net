using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;
using com.parahtml.Core;

namespace com.parahtml.Html
{
    /*
        
        Basically defines Comment and OnDebug.

        All HTML-based renderers should derive from this.

    */
    public abstract class HtmlRenderer : RendererBase
    {
        public HtmlRenderer(HtmlContext context, LineModes lineMode, ContainerModes containerMode, bool indentContent) : base(context, lineMode, containerMode, indentContent)
        {
        }

        protected new HtmlContext Context
        {
            get
            {
                return (HtmlContext)base.Context;
            }
        }

        protected new HtmlOptions Options
        {
            get
            {
                return (HtmlOptions)base.Context.Options;
            }
        }

    }
}
