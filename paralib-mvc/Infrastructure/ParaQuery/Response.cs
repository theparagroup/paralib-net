using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using com.paraquery;

namespace com.paralib.Mvc.Infrastructure.ParaQuery
{
    public class Response : paraquery.Engines.Base.Response
    {
        protected WebViewPage _view { get; private set; }

        public Response(IContext context, WebViewPage view) :base(context)
        {
            _view = view;
        }

        protected override void _write(string text)
        {
            //_view.Write(_view.Html.Raw(text));
            _view.WriteLiteral(text);
        }

    }
}
