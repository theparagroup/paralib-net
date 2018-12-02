using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen;

namespace com.parahtml.Core
{
    //we use this just to keep this method hidden from developers

    public interface IFluentHtmlBase
    {
        void SetContext(HtmlContext context);
    }
}
