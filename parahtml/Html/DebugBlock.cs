using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Html
{
    public class DebugBlock : HtmlRenderer
    {
        public string Name { private set; get; }
        public bool Visible { private set; get; }

        public DebugBlock(HtmlContext context, string name, bool visible) : base(context, LineModes.Multiple, ContainerModes.Block, false)
        {
            Name = name;
            Visible = visible;
        }

        protected override void OnBegin()
        {
            if (Visible)
            {
                Context.Comment($"{Name} begin");
            }
        }

        protected override void OnEnd()
        {
            if (Visible)
            {
                Context.Comment($"{Name} end");
            }
        }
    }
}
