using com.paraquery.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Blocks
{
    public class Script : HtmlBlock
    {
        public Script(IContext context, Tag tag, object attributes = null) : base(context, tag, attributes)
        {
            Begin();
        }

        protected override void OnBegin()
        {
            _tag.Open("script", new { _attributes, defaults = new { type = "text/javascript" } });
        }

        protected override void OnEnd()
        {
            _tag.Close("script");
        }


    }
}
