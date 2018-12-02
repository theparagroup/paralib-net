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

        public CommentBlock(string text) : base(LineModes.Multiple, ContainerModes.Block, false)
        {
            Text = text;
        }

        protected virtual bool IsVisible()
        {
            return true;
        }

        protected override void OnBegin()
        {
            if (IsVisible())
            {
                Comment($"{Text} begin");
            }
        }

        protected override void OnEnd()
        {
            if (IsVisible())
            {
                Comment($"{Text} end");
            }
        }
    }
}
