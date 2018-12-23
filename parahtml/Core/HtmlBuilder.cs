using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;
using com.parahtml.Html;
using System.Text.RegularExpressions;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Core
{
    /*
        
        Convienence methods for creating HTML tags, etc.

    */
    public partial class HtmlBuilder
    {
        protected HtmlContext Context { private set; get; }

        public HtmlBuilder(HtmlContext context)
        {
            Context = context;
        }

        public static string HyphenateUnderscores(string name)
        {
            return name.Replace('_', '-');
        }

        public static string HyphenateMixedCase(string name)
        {
            //mixed case should be replaced with hyphens (but not first letter)
            return Regex.Replace(name, @"([a-z])([A-Z])", "$1-$2");
        }

        public static string SpacenateMixedCase(string name)
        {
            //mixed case should be replaced with spaces (but not first letter)
            return Regex.Replace(name, @"([a-z])([A-Z])", "$1 $2");
        }

        protected virtual AttributeDictionary Attributes<T>(Action<T> attributes = null) where T : GlobalAttributes, new()
        {
            //this method is just to simplify tag methods...
            return Context.AttributeBuilder.Attributes(attributes, null);
        }

        /*
        
            It is possible to create helpers like this to "improve" building tags, but I don't think it's
            very useful, and can't be used in fluent interfaces without overloading everything. 

            public Action<GlobalAttributes> Attributes(string id, string @class)
            {
                return a => { a.Id = id; a.Class = @class; };
            }

            public Action<GlobalAttributes> Attributes(object attributes)
            {
                return a => a.Attributes = attributes; 
            }

        */

        public virtual Tag Block(string name, AttributeDictionary attributes = null, bool empty = false)
        {
            return new Tag(name, attributes, TagTypes.Block, empty);
        }

        public virtual Tag Block(string name, Action<GlobalAttributes> attributes = null, bool empty = false)
        {
            return Block(name, Attributes(attributes), empty);
        }

        public virtual Tag Inline(string name, AttributeDictionary attributes = null, bool empty = false)
        {
            return new Tag(name, attributes, TagTypes.Inline, empty);
        }

        public virtual Tag Inline(string name, Action<GlobalAttributes> attributes = null, bool empty = false)
        {
            return Inline(name, Attributes(attributes), empty);
        }

        public virtual DOCTYPE DOCTYPE(string specification)
        {
            return new DOCTYPE(specification);
        }

        public virtual DOCTYPE DOCTYPE(DocumentTypes documentType)
        {
            return new DOCTYPE(documentType);
        }

        public virtual Tag Html(Action<HtmlAttributes> attributes = null)
        {
            return Block("html", Attributes(attributes));
        }

        public virtual Tag Head(Action<HeadAttributes> attributes = null)
        {
            return Block("head", Attributes(attributes));
        }

        public virtual Tag Title(Action<GlobalAttributes> attributes = null)
        {
            return Inline("title", Attributes(attributes));
        }

        public virtual Tag Link(Action<LinkAttributes> attributes = null)
        {
            return Block("link", Attributes(attributes), true);
        }

        public virtual Tag Meta(Action<MetaAttributes> attributes = null)
        {
            return Block("meta", Attributes(attributes), true);
        }

        public virtual Tag Style(Action<StyleAttributes> attributes = null)
        {
            return Block("style", Attributes(attributes));
        }

        public virtual Tag Body(Action<BodyAttributes> attributes = null)
        {
            return Block("body", Attributes(attributes));
        }

        public virtual Tag Div(Action<GlobalAttributes> attributes = null)
        {
            return Block("div", Attributes(attributes));
        }

        public virtual Tag Span(Action<GlobalAttributes> attributes = null)
        {
            return Inline("span", Attributes(attributes));
        }

        public virtual Tag Br(Action<HrAttributes> attributes = null)
        {
            return Inline("br", Attributes(attributes), true);
        }

        public virtual Tag Hr(Action<HrAttributes> attributes = null)
        {
            return Block("hr", Attributes(attributes), true);
        }

        public virtual Tag Script(Action<ScriptAttributes> attributes = null)
        {
            return Block("script", Attributes(attributes));
        }

        public virtual Tag ExternalScript(Action<ExternalScriptAttributes> attributes = null)
        {
            return new Tag("script", Attributes(attributes), false, LineModes.Single, ContainerModes.None, false);
        }

        public virtual Tag NoScript(Action<GlobalAttributes> attributes = null)
        {
            return Block("noscript", Attributes(attributes));
        }

        public virtual Tag Ul(Action<GlobalAttributes> attributes = null)
        {
            return Block("ul", Attributes(attributes));
        }

        public virtual Tag Ol(Action<GlobalAttributes> attributes = null)
        {
            return Block("ol", Attributes(attributes));
        }

        public virtual Tag Li(Action<GlobalAttributes> attributes = null)
        {
            return Block("li", Attributes(attributes));
        }

        public virtual Tag Img(Action<ImgAttributes> attributes = null)
        {
            return new Tag("img", Attributes(attributes), true, LineModes.None, ContainerModes.Inline, false);

            //return Inline("img", Attributes(attributes), true);
        }

        public virtual Tag A(Action<AAttributes> attributes = null)
        {
            return Inline("a", Attributes(attributes));
        }

        public virtual Tag Form(Action<FormAttributes> attributes = null)
        {
            return Block("form", Attributes(attributes));
        }

        protected virtual Tag Hn(int n, Action<GlobalAttributes> attributes = null)
        {
            return new Tag($"h{n}", Attributes(attributes), false, LineModes.Single, ContainerModes.Block, false);
        }

        public virtual Tag H1(Action<GlobalAttributes> attributes = null)
        {
            return Hn(1, attributes);
        }

        public virtual Tag H2(Action<GlobalAttributes> attributes = null)
        {
            return Hn(2, attributes);
        }

        public virtual Tag H3(Action<GlobalAttributes> attributes = null)
        {
            return Hn(3, attributes);
        }

        public virtual Tag H4(Action<GlobalAttributes> attributes = null)
        {
            return Hn(4, attributes);
        }

        public virtual Tag H5(Action<GlobalAttributes> attributes = null)
        {
            return Hn(5, attributes);
        }

        public virtual Tag H6(Action<GlobalAttributes> attributes = null)
        {
            return Hn(6, attributes);
        }

        public virtual Tag P(Action<GlobalAttributes> attributes = null)
        {
            return Inline("p", Attributes(attributes));
        }

    }
}
