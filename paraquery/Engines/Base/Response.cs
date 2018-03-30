using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Engines.Base
{
    public abstract class Response : IResponse
    {
        protected IContext _context;
        protected int _tabLevel = 0;
        protected List<string> _tabCache;
        protected char _newline;
        protected char _tab;

        public Response(IContext context)
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
                return _tabLevel;
            }
        }

        public virtual void Write(string text)
        {
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

        public void Tabs()
        {
            _write($"{_tabs}");
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

            Tabs();
            Write(text);
        }


    }
}
