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

    public abstract class HtmlBuilder2 : BuilderBase
    {

        public HtmlBuilder2(ContentStack contentStack):base(contentStack)
        {
        }

        public HtmlBuilder2(HtmlBuilder2 htmlBuilder):base(htmlBuilder)
        {
        }

        public new HtmlContext Context
        {
            get
            {
                return (HtmlContext)base.Context;
            }
        }

        private AttributeDictionary Attributes<T>(Action<T> attributes = null) where T : GlobalAttributes, new()
        {
            //this method is just to simplify tag methods...
            return Context.AttributeBuilder.Attributes(attributes, null);
        }

        public IContent Tag(string name, AttributeDictionary attributes, ContainerModes containerMode, LineModes lineMode, bool indentContent, bool empty)
        {
            var tag = new Tag(name, attributes, containerMode, lineMode, indentContent, empty);
            Open(tag);
            return tag;
        }

        public virtual IContent Tag(string name, Action<GlobalAttributes> attributes, ContainerModes containerMode, LineModes lineMode, bool indentContent, bool empty)
        {
            return Tag(name, Attributes(attributes), containerMode, lineMode, indentContent, empty);
        }

        public virtual IContent Block(string name, AttributeDictionary attributes = null)
        {
            return Tag(name, attributes, ContainerModes.Block, LineModes.Multiple, true, false);
        }

        public virtual IContent Block(string name, Action<GlobalAttributes> attributes = null)
        {
            return Block(name, Attributes(attributes));
        }

        public virtual IContent Inline(string name, AttributeDictionary attributes = null)
        {
            return Tag(name, attributes, ContainerModes.Inline, LineModes.None, false, false);
        }

        public virtual IContent Inline(string name, Action<GlobalAttributes> attributes = null)
        {
            return Inline(name, Attributes(attributes));
        }

        public virtual void Empty(string name, AttributeDictionary attributes = null)
        {
            var tag= Tag(name, attributes, ContainerModes.None, LineModes.None, false, true);
            Close(tag);
        }

        public virtual void Empty(string name, Action<GlobalAttributes> attributes = null)
        {
            Empty(name, Attributes(attributes));
        }

        public virtual IContent Div(Action<GlobalAttributes> attributes = null)
        {
            return Block("div", attributes);
        }

        public virtual IContent Div(string @class)
        {
            return Block("div", Attributes<GlobalAttributes>(a=>a.Class=@class));
        }

        public virtual IContent Form(Action<FormAttributes> attributes = null)
        {
            return Block("form", Attributes(attributes));
        }

        public virtual IContent Span(Action<GlobalAttributes> attributes = null)
        {
            return Inline("span", attributes);
        }

        public virtual IContent Span(string @class)
        {
            return Inline("span", Attributes<GlobalAttributes> (a => a.Class = @class));
        }

        public virtual void Hr(Action<HrAttributes> attributes = null)
        {
            var tag = Tag("hr", Attributes(attributes), ContainerModes.None, LineModes.Single, false, true);
            Close(tag);
        }

        public virtual void Br(Action<GlobalAttributes> attributes = null)
        {
            Empty("br", attributes);
        }

        public virtual IContent P(Action<GlobalAttributes> attributes = null)
        {
            return Block("p", Attributes(attributes));
        }

        protected virtual IContent Hn(int n, Action<GlobalAttributes> attributes = null)
        {
            return Tag($"h{n}", Attributes(attributes), ContainerModes.Block, LineModes.Single, false, false);
        }

        public virtual IContent H1(Action<GlobalAttributes> attributes = null)
        {
            return Hn(1, attributes);
        }

        public virtual IContent H2(Action<GlobalAttributes> attributes = null)
        {
            return Hn(2, attributes);
        }

        public virtual IContent H3(Action<GlobalAttributes> attributes = null)
        {
            return Hn(3, attributes);
        }

        public virtual IContent H4(Action<GlobalAttributes> attributes = null)
        {
            return Hn(4, attributes);
        }

        public virtual IContent H5(Action<GlobalAttributes> attributes = null)
        {
            return Hn(5, attributes);
        }

        public virtual IContent H6(Action<GlobalAttributes> attributes = null)
        {
            return Hn(6, attributes);
        }

        public virtual IContent Img(Action<ImgAttributes> attributes = null)
        {
            return Tag("img", Attributes(attributes), ContainerModes.Inline, LineModes.None, false, true);
        }

        public virtual IContent A(Action<AAttributes> attributes = null)
        {
            return Inline("a", Attributes(attributes));
        }
    }
}
