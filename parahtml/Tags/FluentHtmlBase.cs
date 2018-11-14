using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Fluent;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Tags
{
    public partial class FluentHtmlBase<F> : FluentRendererStack<HtmlContext, F> where F : FluentHtmlBase<F>
    {
        public FluentHtmlBase(HtmlContext context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }

        public new HtmlContext Context
        {
            get
            {
                return base.Context;
            }
        }

        protected new HtmlOptions Options
        {
            get
            {
                return (HtmlOptions)base.Context.Options;
            }
        }

        public HtmlBuilder HtmlBuilder
        {
            get
            {
                return Context.HtmlBuilder;
            }
        }

    }
}
