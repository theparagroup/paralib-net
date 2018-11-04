﻿using System;
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

        public HtmlComponent(HtmlContext context, Renderer starter) : base(context, starter)
        {
            TagBuilder = new TagBuilder(context);
        }

        protected new HtmlContext Context
        {
            get
            {
                return (HtmlContext)base.Context;
            }
        }

        protected override void OnDebug(string text)
        {
            HtmlRenderer.Comment(Writer, text);
        }

    }
}
