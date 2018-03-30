using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Blocks
{
    public class Div : HtmlBlock
    {

        public Div(IContext context, Tag tag, object attributes = null) : base(context, tag, attributes)
        {
            Begin();
        }

        protected override void OnBegin()
        {
            _tag.Open("div", _attributes,true);
        }

        protected override void OnEnd()
        {
            _tag.Close("div", true);
        }


    }
}
