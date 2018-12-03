using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Builders;
using com.paralib.Gen.Rendering;
using com.parahtml.Attributes;
using com.parahtml.Html;
using com.parahtml.Core;
using com.paralib.Gen;

namespace com.parahtml.Html2
{



    public abstract class HtmlBuilder2<C> : BuilderBase<C> where C:HtmlContext
    {
        public HtmlBuilder2(RendererStack rendererStack) : base(rendererStack)
        {
        }

        private AttributeDictionary Attributes<T>(Action<T> attributes = null) where T : GlobalAttributes, new()
        {
            //this method is just to simplify tag methods...
            return Context.AttributeBuilder.Attributes(attributes, null);
        }

        private Tag Tag(string name, AttributeDictionary attributes, TagTypes tagType, bool empty)
        {
            var tag = new Tag(name, attributes, tagType, empty);
            ((IRenderer)tag).SetContext(Context);
            ((IBuilderBase<C>)this).Open(tag);
            return tag;
        }

        private Closable Block(string name, AttributeDictionary attributes = null, bool empty = false)
        {
            return new Closable(Tag(name, attributes, TagTypes.Block, empty));
        }

        private void Inline(string name, AttributeDictionary attributes = null, bool empty = false)
        {
            Tag(name, attributes, TagTypes.Inline, empty);
        }

        public virtual Closable Block(string name, Action<GlobalAttributes> attributes = null, bool empty = false)
        {
            return Block(name, Attributes(attributes), empty);
        }

        public virtual void Inline(string name, Action<GlobalAttributes> attributes = null, bool empty = false)
        {
            Inline(name, Attributes(attributes), empty);
        }

        public virtual Closable Div(Action<GlobalAttributes> attributes = null)
        {
            return Block("div", Attributes(attributes));
        }

        public virtual Closable Div(string @class)
        {
            return Block("div", Attributes<GlobalAttributes>(a=>a.Class=@class));
        }

        public virtual void Span(Action<GlobalAttributes> attributes = null)
        {
            Inline("span", Attributes(attributes));
        }

        public virtual void Span(string @class)
        {
            Inline("span", Attributes<GlobalAttributes> (a => a.Class = @class));
        }

        public virtual Closable Hr(Action<HrAttributes> attributes = null)
        {
            return Block("hr", Attributes(attributes), true);
        }

        public virtual void Br(Action<HrAttributes> attributes = null)
        {
            Inline("br", Attributes(attributes), true);
        }


    }
}
