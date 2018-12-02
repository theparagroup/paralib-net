using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;

namespace com.parahtml.Attributes
{
    //we use this just to keep this method hidden from developers
    public interface IGlobalAttributes
    {
        void SetContext(HtmlContext context);

    }
}
