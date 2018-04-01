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
        public bool IsEmpty { private set; get; }

        public Element(IContext context, TagBuilder tagBuilder, ElementTypes elementType, string name, object attributes = null, bool empty=false) : base(context, elementType==ElementTypes.Block)
        {
            _tagBuilder = tagBuilder;
            ElementType = elementType;
            Name = name;
            Attributes =attributes;
            IsEmpty = empty;
        }

        protected override void OnBegin()
        {
            if (IsEmpty)
            {
                _tagBuilder.Empty(Name, Attributes, ElementType == ElementTypes.Block);
            }
            else
            {
                _tagBuilder.Open(Name, Attributes, ElementType == ElementTypes.Block);
            }
        }

        protected override void OnEnd()
        {
            if (!IsEmpty)
            {
                _tagBuilder.Close(Name, ElementType == ElementTypes.Block);
            }
        }


    }
}
