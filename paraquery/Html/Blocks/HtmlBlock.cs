using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;

namespace com.paraquery.Html.Blocks
{
    public abstract class HtmlBlock : Block
    {
        protected object _attributes;

        public HtmlBlock(IContext context, object attributes = null) : base(context)
        {
            _attributes = attributes;
        }




    }
}
