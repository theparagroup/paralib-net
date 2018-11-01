using System;
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
        protected IWriter _writer;

        public TagBuilder(Context context)
        {
            _context = context;
            _writer = _context.Writer;
        }

        public Context Context
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

        public virtual void Open(string name, bool empty, AttributeDictionary attributes = null)
        {
            _writer.Write($"<{name}");

            Attributes(attributes);

            if (!empty)
            {
                _writer.Write(">");
            }

        }

        public virtual void Close(string name, bool empty)
        {
            if (empty)
            {
                if (_context.Options.SelfClosingTags)
                {
                    _writer.Write(" />");
                }
                else
                {
                    _writer.Write(">");
                }
            }
            else
            {
                _writer.Write($"</{name}>");
            }

        }

    }
}
