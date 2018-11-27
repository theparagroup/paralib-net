using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Html
{
    public class CommentBlock : HtmlRenderer
    {
        public string Text { private set; get; }
        public bool Visible { private set; get; }

        public CommentBlock(HtmlContext context, string text, bool visible) : base(context, LineModes.Multiple, ContainerModes.Block, false)
        {
            Text = text;
            Visible = visible;
        }

        protected override void OnBegin()
        {
            if (Visible)
            {
                Comment($"{Text} begin");
            }
        }

        protected override void OnEnd()
        {
            if (Visible)
            {
                Comment($"{Text} end");
            }
        }
    }
}
