using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;

namespace com.paraquery.Html
{
    public class BlockElement : Element
    {
        public BlockElement(IContext context, TagBuilder tagBuilder, string name, object attributes = null, bool empty = false) : base(context, tagBuilder, ElementTypes.Block, name, attributes, empty)
        {
            Begin();
        }
    }
}
