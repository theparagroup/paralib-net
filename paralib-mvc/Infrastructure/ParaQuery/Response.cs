using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;
using System.Web.Mvc;

namespace com.paralib.Mvc.Infrastructure.ParaQuery
{
    public class Response : ResponseBase
    {
        protected WebViewPage _view;
        protected int _tabLevel = 0;
        protected string _tabCache;


        public Response(WebViewPage view)
        {
            _view = view;
        }

        protected override void _write(string text)
        {
            //_view.Write(_view.Html.Raw(text));
            _view.WriteLiteral(text);
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
    }
}
