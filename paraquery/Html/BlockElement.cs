using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;
using com.paraquery.Html.Tags;


namespace com.paraquery.Html
{
    public class BlockElement : Element
    {
        public BlockElement(IContext context, TagBuilder tagBuilder, string name, object attributes = null, bool empty = false, bool render=true) : base(context, tagBuilder, ElementTypes.Block, name, attributes, empty, render)
        {
            Begin();
        }
    }
}
