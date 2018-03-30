using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Blocks
{
    public abstract class HtmlBlock : Block
    {
        protected Tag _tag;
        protected object _attributes;

        public HtmlBlock(IContext context, Tag tag, object attributes = null) : base(context)
        {
            _tag = tag;
            _attributes = attributes;
        }

    }
}
