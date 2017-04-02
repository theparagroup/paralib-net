using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;


namespace com.paraquery.StringContext
{
    public class Response : ResponseBase
    {
        protected StringBuilder _sb { get; }= new StringBuilder();
        protected int _tabLevel = 0;
        protected string _tabCache;

        protected override void _write(string text)
        {
            _sb.Append(text);
        }

        protected override string _tabs
        {
            get
            {
                return _tabCache;
            }

        }

        public override void NewLine()
        {
            _write("\n");
        }

        public override void Tab()
        {
            _write("\t");
        }

        public override void Indent()
        {
            ++_tabLevel;
            _tabCache = new string('\t', _tabLevel);
        }

        public override void Dedent()
        {
            --_tabLevel;
            _tabCache = new string('\t', _tabLevel);
        }

        public override string ToString()
        {
            return _sb.ToString();
        }

    }
}
