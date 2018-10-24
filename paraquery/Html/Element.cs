using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;
using com.paraquery.Rendering;

namespace com.paraquery.Html
{
    public class Element : Renderer
    {
        protected TagBuilder _tagBuilder;
        protected string _tagName;
        public object Attributes { private set; get; }

        public Element(TagBuilder tagBuilder, RendererTypes rendererType, string tagName, object attributes, bool empty=false) : base(tagBuilder.Context, rendererType, empty)
        {
            _tagBuilder = tagBuilder;
            _tagName = tagName;
            Attributes = attributes;
        }

        protected override void Debug(string message)
        {
            _writer.Write($" <!-- {message} {_tagName} {Id} -->");
        }

        public string Id
        {
            get
            {
                //TODO make this work correctly with anonymous objects, etc
                return _tagBuilder.GetAttribute(Attributes, "id");
            }
        }

        protected override void OnBegin()
        {
            if (IsEmpty)
            {
                _tagBuilder.Empty(_tagName, Attributes);
            }
            else
            {
                _tagBuilder.Open(_tagName, Attributes);
            }
        }

        protected override void OnEnd()
        {
            if (!IsEmpty)
            {
                _tagBuilder.Close(_tagName);
            }
        }

    }
}
