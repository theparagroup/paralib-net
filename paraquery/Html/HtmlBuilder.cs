﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;
using com.paraquery.Html.Tags.Attributes;

namespace com.paraquery.Html
{
    /*
        
        Convienence methods for creating HTML tags, etc.

    */
    public partial class HtmlBuilder
    {
        protected Context Context { private set; get; }

        public HtmlBuilder(Context context)
        {
            Context = context;
        }

        protected virtual AttributeDictionary Attributes<T>(Action<T> attributes = null) where T : GlobalAttributes, new()
        {
            //this method is just to simplify tag methods...
            return AttributeDictionary.Attributes(attributes, null);
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
            return new Tag(Context, TagTypes.Block, name, attributes, empty);
        }

        public virtual Tag Block(string name, Action<GlobalAttributes> attributes = null, bool empty = false)
        {
            return Block(name, Attributes(attributes), empty);
        }

        public virtual Tag Inline(string name, AttributeDictionary attributes = null, bool empty = false)
        {
            return new Tag(Context, TagTypes.Inline, name, attributes, empty);
        }

        public virtual Tag Inline(string name, Action<GlobalAttributes> attributes = null, bool empty = false)
        {
            return Inline(name, Attributes(attributes), empty);
        }

        public virtual DOCTYPE DOCTYPE(string specification)
        {
            return new DOCTYPE(Context, specification);
        }

        public virtual DOCTYPE DOCTYPE(DocumentTypes documentType)
        {
            return new DOCTYPE(Context, documentType);
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

    }
}