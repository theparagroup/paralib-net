using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;
using com.paraquery.Blocks;

namespace com.paraquery.Html
{
    public abstract class Element : Block
    {
        protected TagBuilder _tagBuilder;
        public ElementTypes ElementType { private set; get; }
        public string Name { private set; get; }
        public object Attributes { private set; get; }
        public bool Render { private set; get; }
        public string Extra { set; get; }

        public Element(IContext context, TagBuilder tagBuilder, ElementTypes elementType, string name, object attributes, bool empty, bool render) : base(context, (elementType==ElementTypes.Block), empty)
        {
            _tagBuilder = tagBuilder;
            ElementType = elementType;
            Name = name;
            Attributes =attributes;
            Render = render;
        }

        protected override void OnBegin()
        {
            //sometimes it is useful to have "invisible" elements, like in the fluentgrid
            if (Render)
            {
                if (IsEmpty)
                {
                    _tagBuilder.Empty(Name, Attributes, (ElementType == ElementTypes.Block));
                    _response.Dedent();
                }
                else
                {
                    _tagBuilder.Open(Name, Attributes, (ElementType == ElementTypes.Block));
                }
            }
        }

        protected override void OnEnd()
        {
            if (Render)
            {
                if (!IsEmpty)
                {
                    _tagBuilder.Close(Name, (ElementType == ElementTypes.Block));
                }
            }
        }


    }
}
