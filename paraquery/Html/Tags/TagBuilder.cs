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
        protected IContext _context;
        protected IWriter _writer;

        public TagBuilder(IContext context)
        {
            _context = context;
            _writer = _context.Writer;
        }

        public IContext Context
        {
            get
            {
                return _context;
            }
        }

        protected virtual AttributeDictionary Attributes<T>(Action<T> init = null) where T : GlobalAttributes, new()
        {
            //this method is just to simplify tag methods...
            return AttributeDictionary.Attributes(init, null);
        }

        public virtual void Attribute(string name, string value = null)
        {
            //TODO escaping quotes? escaping in general?

            if (name != null)
            {
                if (value == null)
                {
                    //boolean
                    _writer.Write($" {name}");
                }
                else
                {
                    _writer.Write($" {name}=\"{value}\"");
                }

            }
        }

        public virtual void Attributes(AttributeDictionary dictionary)
        {
            if (dictionary != null)
            {
                foreach (var name in dictionary.Keys)
                {
                    Attribute(name, dictionary[name]);
                }
            }
        }

        public virtual void Start(string name, AttributeDictionary attributes = null)
        {
            _writer.Write($"<{name}");
            Attributes(attributes);
        }

        public virtual void End()
        {
            _writer.Write(">");
        }

        public virtual void Open(string name, AttributeDictionary attributes = null)
        {
            Start(name, attributes);
            End();
        }

        public virtual void Close(string name)
        {
            _writer.Write($"</{name}>");
        }

        public virtual void Empty(string name, AttributeDictionary attributes = null)
        {
            _writer.Write($"<{name}");

            Attributes(attributes);

            if (_context.Options.SelfClosingTags)
            {
                _writer.Write(" />");
            }
            else
            {
                _writer.Write(">");
            }

        }


       


    }
}
