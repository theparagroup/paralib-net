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
        protected bool _isTabbed; //we have tabbed out the indention for this line
        protected bool _isNewLine; //we are at the start of a new line
        protected bool _isSpaced; //last output was blank line

        protected abstract void _write(string text);
        protected abstract void _writeLine();
        protected abstract void _writeLine(string text);

        protected virtual string _newline
        {
            get
            {
                return Environment.NewLine;
            }
        }

        public Writer(IContext context)
        {
            _context = context;

            //these can be changed in derived classes
            _tab = '\t';

            //we start off with a newline
            _isNewLine = true;

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


        public virtual void Write(string text)
        {
            if (!_isTabbed)
            {
                _write($"{GetTabs()}");
                _isTabbed = true;
            }

            _write(text);

            _isNewLine = false;
            _isSpaced = false;
        }

        public virtual void WriteLine(string text)
        {
            if (!_isTabbed)
            {
                _write($"{GetTabs()}");
            }

            _write(text);

            if (_isNewLine && text=="")
            {
                //this is the same as two NewLine() calls
                _isSpaced = true;
            }
            else
            {
                _isSpaced = false;
            }

            _writeLine("");

            _isNewLine = true;
            _isTabbed = false;
        }

        public void Space()
        {
            if (!_isSpaced)
            {
                if (!_isNewLine)
                {
                    NewLine();
                }

                NewLine();

                //the second NewLine() will set _isSpaced, etc
            }
        }


        public virtual void Snippet(string text, string newline = null)
        {
            if (true)
            {
                //tabify

                if (newline==null)
                {
                    newline = Environment.NewLine;
                }

                text = text.Replace($"{newline}", $"{_newline}{GetTabs()}");
                _write($"{GetTabs()}");
            }

            _write(text);

            _isNewLine = false;
            _isSpaced = false;
            _isTabbed = true;

        }

        public void NewLine()
        {
            //if we are already at a newline, then another newline will space things out
            //otherwise we're just newline and not spaced
            if (_isNewLine)
            {
                _isSpaced = true;
            }
            else
            {
                _isSpaced = false;
            }

            _writeLine("");

            _isNewLine = true;
            _isTabbed = false;
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


    }
}

