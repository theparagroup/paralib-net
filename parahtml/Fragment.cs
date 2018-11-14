﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.parahtml.Tags.Fluent;
using com.paralib.Gen.Fluent;
using com.parahtml.Tags;

namespace com.parahtml
{
    public class Fragment : FluentHtmlBase<Fragment>, IDisposable
    {
        public Fragment(HtmlContext context) : base(context, new RendererStack())
        {
        }

        public Css.Fluent.Css Css()
        {
            var cssContext = new Css.CssContext(Context);
            var css = new Css.Fluent.Css(cssContext, _rendererStack);
            return css;
        }

        public IDocument Document()
        {
            return new Document(Context, _rendererStack);
        }

        public Html Html()
        {
            return new Html(Context, _rendererStack);
        }

        public void Dispose()
        {
            CloseAll();
        }

    }
}
