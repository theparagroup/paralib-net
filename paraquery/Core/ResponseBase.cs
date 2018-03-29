using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Core
{
    public abstract class ResponseBase : IResponse
    {
        protected IContext _context;
        protected int _tabLevel = 0;
        protected List<string> _tabCache;
        protected char _newline;
        protected char _tab;

        public ResponseBase(IContext context)
        {
            _context = context;

            //these can be overridden in derived classes
            _tab = '\t';
            _newline = '\n';

            //init tab cache
            _tabCache = new List<string>();
            _tabCache.Add(""); //0
            _tabCache.Add($"{_tab}"); //1
            _tabCache.Add($"{_tab}{_tab}"); //2
            _tabCache.Add($"{_tab}{_tab}{_tab}"); //3

        }

        protected abstract void _write(string text);

        protected string _tabs
        {
            get
            {
                return _tabCache[_tabLevel];
            }
        }

        public int TabLevel
        {
            get
            {
                return _tabLevel; ;
            }
        }

        public virtual void Write(string text)
        {
            _write(_tabs);
            _write(text);
        }

        public virtual void WriteLine(string text)
        {
            Write(text);
            NewLine();
        }

        public void NewLine()
        {
            _write($"{_newline}");
        }

        public void Tab()
        {
            _write($"{_tab}");
        }

        public void Indent()
        {
            ++_tabLevel;

            if (_tabLevel>=_tabCache.Count)
            {
                _tabCache.Add(new string(_tab, _tabLevel));
            }

        }

        public void Dedent()
        {
            //this will throw an error, for example, if you call end without begin
            //kind of confusing error so we just don't do it here
            //TODO do it elsewhere (Block)
            //if (_tabLevel>=0)
            {
                --_tabLevel;
            }
        }

        public virtual void Snippet(string name, string text)
        {
            text = text.Replace($"{_newline}", $"{_newline}{_tabs}");

            _write(_tabs);
            _write(text);
            NewLine();
        }

        public virtual void Attribute(string name, string value)
        {
            if (name != null && value != null)
            {
                _write($" {name}=\"{value}\"");
            }
        }

        public void Attributes(object attributes = null)
        {
            //namespace state should be over in context

            //TODO allow for namespaces
            //TODO allow for namespace-vars (class="[admin:foo-bar]" -> class="nsvar-foo-bar") 
            //TODO allow for name substitutions (clazz->class, classes->class)
            //TODO allow for symbol replacement (_,-) in names ( data_value -> data-value, data__value -> data_value)
            //TODO allow for variables? (class="{debug}" -> class="debug-verbose")
            //TODO allow for expansions? new { id="foo", style=new {background_color="green"}, margin=new { border_style="solid" } } -> id="foo" style="backgound-color:green;" margin="border-style:solid;"

            // id="foo-bar" -> id="foo-bar" (no change)
            // id="[foo-bar]" -> id="ns-foo-bar" (ns prefixing)
            // id="[blah:admin:foo-bar]" -> id="blah-admin-foo-bar" (if admin not an nsvar)
            // id="[:blah:admin:foo-bar]" -> id="ns-blah-admin-foo-bar" (with current ns)
            // id="[blah:admin:foo-bar]" -> id="blah-nsvar-foo-bar" (if admin is an nsvar)

            var atts = Html.Attribute.ToDictionary(attributes);

            if (atts != null)
            {
                if (atts.ContainsKey("id"))
                {
                    //TODO process namespaces for ids
                    Attribute("id", atts["id"]);
                }

                if (atts.ContainsKey("class"))
                {
                    //TODO process namespaces for classes
                    Attribute("class", atts["class"]);
                }

                foreach (var key in atts.Keys)
                {
                    if (key != "id" && key != "class")
                    {
                        Attribute(key, atts[key]);
                    }
                }

            }
        }
       

        public virtual void StartBlock(string name)
        {
            Write($"<{name}");
        }

        public virtual void EndBlock()
        {
            _write(">");
            NewLine();
        }

        public virtual void CloseBlock(string name)
        {
            NewLine();
            Write($"</{name}>");
            NewLine();
        }

        public virtual void StartInline(string name)
        {
            Write($"<{name}");
        }

        public virtual void EndInline()
        {
            Write(">");
        }

        public virtual void CloseInline(string name)
        {
            Write($"</{name}>");
        }

        public virtual void Empty(string name)
        {
            Write($"<{name} />");
        }

    }
}
