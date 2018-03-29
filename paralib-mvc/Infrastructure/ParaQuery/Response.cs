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
