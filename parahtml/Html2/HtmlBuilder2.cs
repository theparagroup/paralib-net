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

        private Tag Tag(string name, AttributeDictionary attributes, TagTypes tagType, bool empty)
        {
            var tag = new Tag(name, attributes, tagType, empty);
            Open(tag);
            return tag;
        }

        private Renderer Block(string name, AttributeDictionary attributes = null, bool empty = false)
        {
            return Tag(name, attributes, TagTypes.Block, empty);
        }

        private Renderer Inline(string name, AttributeDictionary attributes = null, bool empty = false)
        {
            return Tag(name, attributes, TagTypes.Inline, empty);
        }

        public virtual Renderer Block(string name, Action<GlobalAttributes> attributes = null, bool empty = false)
        {
            return Block(name, Attributes(attributes), empty);
        }

        public virtual Renderer Inline(string name, Action<GlobalAttributes> attributes = null, bool empty = false)
        {
            return Inline(name, Attributes(attributes), empty);
        }

        public virtual Renderer Div(Action<GlobalAttributes> attributes = null)
        {
            return Block("div", Attributes(attributes));
        }

        public virtual Renderer Div(string @class)
        {
            return Block("div", Attributes<GlobalAttributes>(a=>a.Class=@class));
        }

        public virtual Renderer Span(Action<GlobalAttributes> attributes = null)
        {
            return Inline("span", Attributes(attributes));
        }

        public virtual Renderer Span(string @class)
        {
            return Inline("span", Attributes<GlobalAttributes> (a => a.Class = @class));
        }

        public virtual Renderer Hr(Action<HrAttributes> attributes = null)
        {
            return Block("hr", Attributes(attributes), true);
        }

        public virtual Renderer Br(Action<HrAttributes> attributes = null)
        {
            //return Inline("br", Attributes(attributes), true);
            var tag= new Tag("br", Attributes(attributes), TagTypes.Inline, true, LineModes.None, ContainerModes.Inline, false);
            Open(tag);
            Close();
            return tag;
        }


    }
}
