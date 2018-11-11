using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    /*

        This base class for writers is where most of the state management for formatting
        is performed. Other classes like Renderers and RendererStacks are totally dependent
        upon the correct Writer behavior.

        Formatting includes:
            newlines after blocks and lines
            indented block content
            "spacing" - ensuring there is one and only one newline between written lines

        We cache the tabs for some attempt at efficiency.

        Everything is virtual, but probably no need (nor is it a good idea) to change the
        way things are working. 

        However, the one thing Writer doesn't know how to do... is actually write.

        A derived class must implement:
            _write
            _writeLine

        A derivied class can also override the newline string, which defaults to the 
        environment settng. 
        
        HTTP and HTML generally use CRLF, but other applications may diverge from the
        environment (OS) setting.

        There is no mechanism to change writer settings mid-stream so to speak, you
        would want to create two writers and merge the output (with a string buffer,
        for example).


    */
    public abstract class Writer
    {
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

        public Writer()
        {
            Init();
        }

        protected virtual void Init()
        {
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
            _tabCache.Add($"{_tab}{_tab}{_tab}{_tab}{_tab}{_tab}{_tab}"); //7
            _tabCache.Add($"{_tab}{_tab}{_tab}{_tab}{_tab}{_tab}{_tab}{_tab}"); //8
            _tabCache.Add($"{_tab}{_tab}{_tab}{_tab}{_tab}{_tab}{_tab}{_tab}{_tab}"); //9
            _tabCache.Add($"{_tab}{_tab}{_tab}{_tab}{_tab}{_tab}{_tab}{_tab}{_tab}{_tab}"); //10
        }

        public virtual int TabLevel
        {
            get
            {
                return _tabLevel;
            }
        }

        public virtual bool IsNewLine
        {
            get
            {
                return _isNewLine;
            }
        }

        protected virtual string GetTabs(int? level = null)
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

        public virtual void Tab()
        {
            //tabbing like this doesn't change state - use sparingly
            _write($"{_tab}");
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

        public virtual void Space()
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

        public virtual void NewLine()
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

        public virtual void Indent()
        {
            ++_tabLevel;

            if (_tabLevel >= _tabCache.Count)
            {
                _tabCache.Add(new string(_tab, _tabLevel));
            }

        }

        public virtual void Dedent()
        {
            //obviously we can't have negative tabs
            if (_tabLevel > 0)
            {
                --_tabLevel;
            }
        }


    }
}

