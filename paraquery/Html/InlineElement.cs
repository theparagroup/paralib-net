using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;

namespace com.paraquery.Html
{
    public class InlineElement : Element
    {
        public InlineElement(IContext context, TagBuilder tagBuilder, string name, object attributes = null, bool empty = false, bool render=true) : base(context, tagBuilder, ElementTypes.Inline, name, attributes, empty, render)
        {
            Begin();
        }
    }
}
