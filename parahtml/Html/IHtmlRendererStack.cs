using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;

namespace com.parahtml.Html
{
    public interface IHtmlRendererStack
    {
        void SetContext(HtmlContext context);
    }
}
