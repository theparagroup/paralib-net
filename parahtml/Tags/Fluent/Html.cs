using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Tags.Fluent
{
    /*

        FluentHtml must either be in Block or Inline mode because....

        Since adding HTML elements under an empty elment doesn't make any sense, we don't have
        that concept (like Tag does) and never go into the "Single" LineMode.

    */
    public partial class Html : HtmlComponent<ParaHtmlPackage>
    {
        public Html(HtmlContext context, LineModes lineMode, ContainerModes containerMode, bool begin = true) : base(context, lineMode, containerMode, false, false)
        {
            if (begin)
            {
                Begin();
            }
        }

        public override string Name
        {
            get
            {
                return "fluent html";
            }
        }

        protected override void OnBegin()
        {
            if (Visible)
            {
                Comment("fluent html start");
            }
        }

        protected override void OnEnd()
        {
            if (Visible)
            {
                Comment("fluent html end");
            }
        }


        protected override void Comment(string text)
        {
            HtmlRenderer.HtmlComment(Writer, text);
        }


        protected new Html Push(Renderer renderer)
        {
            //this method is just to simplify fluent methods...
            base.Push(renderer);
            return this;
        }

        public new Html Open(Renderer renderer)
        {
            return Push(renderer);
        }

        public new Html CloseUp()
        {
            base.CloseUp();
            return this;
        }

        public new Html CloseBlock()
        {
            base.CloseBlock();
            return this;
        }

        public new Html CloseAll()
        {
            base.CloseAll();
            return this;
        }

        public new Html Close()
        {
            base.Close();
            return this;
        }

        public new Html Close(Renderer renderer)
        {
            base.Close(renderer);
            return this;
        }

      

    }
}