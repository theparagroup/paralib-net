﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.parahtml.Packages;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Html
{
    /*
        TagType is an HtmlComponent that is also an IRenderer and has its own stack.

        TagTypes are Inline or Block, we don't support empty (None) elements as it
        doesn't make any sense (just create a custom HtmlRenderer instead).

        The component itself isn't visible but can be pushed and popped according
        to the usual rules.

    */
    public abstract class TagComponent<P> : HtmlRendererComponent<TagComponent<P>, P> where P : Package, new()
    {
        protected Tag _tag;

        public TagComponent(Tag tag, bool indentContent) : base(((Renderer)tag).LineMode, ((Renderer)tag).ContainerMode, indentContent)
        {
            _tag = tag;
        }

        protected override void OnBeginContent()
        {
            Open(_tag);
        }

        protected override void OnEndContent()
        {
            Close(_tag);
        }



    }
}
