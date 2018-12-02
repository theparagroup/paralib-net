using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml.Core
{
    public interface IHasContext
    {
        void SetContext(HtmlContext context);
    }
}
