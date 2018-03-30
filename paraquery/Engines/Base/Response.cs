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
        protected bool _isNewLine;
        protected bool _isSpaced;

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
            _tabCache.Add($"{_tab}{_tab}{_tab}{_tab}"); //4
            _tabCache.Add($"{_tab}{_tab}{_tab}{_tab}{_tab}"); //5
            _tabCache.Add($"{_tab}{_tab}{_tab}{_tab}{_tab}{_tab}"); //6

        }

        protected abstract void _write(string text);

        protected string _tabs
        {
            get
            {
                return _tabCache[_tabLevel];
            }
        }


        public virtual void Write(string text, bool indent = true)
        {
            if (indent)
            {
                _write($"{_tabs}");
            }

            _write(text);
            _isNewLine = false;
            _isSpaced = false;
        }

        public virtual void WriteLine(string text, bool indent=true)
        {
            if (indent)
            {
                _write($"{_tabs}");
            }

            _write(text);

            if (_isNewLine)
            {
                _isSpaced = true;
            }

            _write($"{_newline}");
            _isNewLine = true;
        }

        public virtual void Snippet(string name, string text, bool indent = true)
        {
            if (indent)
            {
                text = text.Replace($"{_newline}", $"{_newline}{_tabs}");
                _write($"{_tabs}");
            }

            _write(text);
            _isNewLine = false;
        }

        public void NewLine()
        {

            if (_isNewLine)
            {
                _isSpaced = true;
            }

            _write($"{_newline}");
            _isNewLine = true;
        }

        public void Tab()
        {
            _write($"{_tab}");
            _isNewLine = false;
        }

        public void Tabs()
        {
            _write($"{_tabs}");
            _isNewLine = false;
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

        public int TabLevel
        {
            get
            {
                return _tabLevel;
            }
        }

        public bool IsNewLine
        {
            get
            {
                return _isNewLine;
            }
        }

        public bool IsSpaced
        {
            get
            {
                return _isSpaced;
            }
        }

    }
}
