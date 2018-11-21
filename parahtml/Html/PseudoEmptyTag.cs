using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Html
{
    public class PseudoEmptyTag : HtmlRenderer
    {
        public string TagName { private set; get; }
        public AttributeDictionary Attributes { private set; get; }

        public PseudoEmptyTag(HtmlContext context, string tagName, AttributeDictionary attributes) : base(context, LineModes.Single, ContainerModes.Block, false)
        {
            TagName = tagName;
            Attributes = attributes;

            if (TagName == null)
            {
                throw new InvalidOperationException("TagName cannot be null");
            }
        }

        protected override void OnBegin()
        {
            Writer.Write($"<{TagName}");

            if (Attributes != null)
            {
                var attributes = Context.AttributeBuilder.ToTagContents(Attributes);
                Writer.Write($" ");
                Writer.Write(attributes);
            }

            Writer.Write(">");
        }

        protected override void OnEnd()
        {
            Writer.Write($"</{TagName}>");
        }

    }
}
