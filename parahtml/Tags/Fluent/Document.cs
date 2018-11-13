using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Tags.Fluent
{
    public partial class Document : HtmlComponent<ParaHtmlPackage>
    {
        public Document(HtmlContext context, bool begin = true) : base(context, LineModes.Multiple, ContainerModes.Block, false, false)
        {
        }

        public override string Name
        {
            get
            {
                return "Document";
            }
        }

        protected override void Comment(string text)
        {
            HtmlRenderer.HtmlComment(Writer, text);
        }

        protected override void OnBegin()
        {
        }

        protected override void OnEnd()
        {
        }

        protected new Document Push(Renderer renderer)
        {
            //this method is just to simplify fluent methods...
            base.Push(renderer);
            return this;
        }


    }
}
