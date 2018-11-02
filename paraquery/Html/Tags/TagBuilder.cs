﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;

namespace com.paraquery.Html.Tags
{

    public partial class TagBuilder
    {
        protected Context _context;

        public TagBuilder(Context context)
        {
            _context = context;
        }

        protected virtual AttributeDictionary Attributes<T>(Action<T> init = null) where T : GlobalAttributes, new()
        {
            //this method is just to simplify tag methods...
            return AttributeDictionary.Attributes(init, null);
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

        public virtual Tag Block(string name, Action<GlobalAttributes> attributes = null, bool empty = false)
        {
            return new Tag(_context, name, true, empty, Attributes(attributes));
        }

        public virtual Tag Inline(string name, Action<GlobalAttributes> attributes = null, bool empty = false)
        {
            return new Tag(_context, name, false, empty, Attributes(attributes));
        }

        public virtual Tag Html(Action<HtmlAttributes> attributes = null)
        {
            return new Tag(_context, "html", true, false, Attributes(attributes));
        }

        public virtual Tag Head(Action<HeadAttributes> attributes = null)
        {
            return new Tag(_context, "head", true, false, Attributes(attributes));
        }

        public virtual Tag Title(Action<GlobalAttributes> attributes = null)
        {
            return new Tag(_context, "title", false, false, Attributes(attributes));
        }

        public virtual Tag Body(Action<BodyAttributes> attributes = null)
        {
            return new Tag(_context, "body", true, false, Attributes(attributes));
        }

        public virtual Tag Div(Action<GlobalAttributes> attributes = null)
        {
            return new Tag(_context, "div", true, false, Attributes(attributes));
        }

        public virtual Tag Span(Action<GlobalAttributes> attributes = null)
        {
            return new Tag(_context, "span", false, false, Attributes(attributes));
        }

        public virtual Tag Hr(Action<HrAttributes> attributes = null)
        {
            return new Tag(_context, "hr", true, true, Attributes(attributes));
        }

        public virtual Tag Script(Action<ScriptAttributes> attributes = null)
        {
            return new Tag(_context, "script", true, false, Attributes(attributes));
        }

    }
}
