using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Engines.Base
{

    public abstract class Writer : IWriter
    {
        protected IContext _context;
        protected int _tabLevel = 0;
        protected List<string> _tabCache;
        protected char _tab;
        protected bool _isNewLine;
        protected bool _isSpaced;

        protected abstract void _write(string text);
        protected abstract void _writeLine();
        protected abstract void _writeLine(string text);
        protected abstract string _newline { get; }

        public Writer(IContext context)
        {
            _context = context;

            //these can be changed in derived classes
            _tab = '\t';

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

        protected string GetTabs(int? level = null)
        {
            if (level == null)
            {
                level = _tabLevel;
            }
            else
            {
                if (level.Value >= _tabCache.Count)
                {
                    //this is rare, so don't expand the cache, just generate
                    return new string(_tab, level.Value);
                }
            }

            return _tabCache[level.Value];
        }


        public virtual void Write(string text, bool indent = true)
        {
            if (indent)
            {
                _write($"{GetTabs()}");
            }

            _write(text);
            _isNewLine = false;
            _isSpaced = false;
        }

        public virtual void WriteLine(string text, bool indent = true)
        {
            if (indent)
            {
                _write($"{GetTabs()}");
            }

            _write(text);

            if (_isNewLine)
            {
                _isSpaced = true;
            }

            _writeLine("");
            _isNewLine = true;
        }

        public virtual void Snippet(string name, string text, bool indent = true, string newline = "\n")
        {
            if (indent)
            {
                text = text.Replace($"{newline}", $"{_newline}{GetTabs()}");
                _write($"{GetTabs()}");
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

            _writeLine("");
            _isNewLine = true;
        }

        public void Tab()
        {
            _write($"{_tab}");
            _isNewLine = false;
        }

        public void Tabs(int? level)
        {
            _write($"{GetTabs(level)}");
            _isNewLine = false;
        }

        public void Indent()
        {
            ++_tabLevel;

            if (_tabLevel >= _tabCache.Count)
            {
                _tabCache.Add(new string(_tab, _tabLevel));
            }

        }

        public void Dedent()
        {
            //obviously we can't have negative tabs
            if (_tabLevel > 0)
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

