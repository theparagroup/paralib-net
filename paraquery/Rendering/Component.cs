using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;

namespace com.paraquery.Rendering
{
    public abstract class Component:RendererStack
    {
        protected TagBuilder _tagBuilder;

        public Component(TagBuilder tagBuilder, RenderModes renderModes, bool visible) : base(tagBuilder.Context, renderModes, visible)
        {
            _tagBuilder = tagBuilder;
        }

        protected override void OnPostBegin()
        {
            base.OnPostBegin();

            OnBeforeContent();
        }

        protected abstract void OnBeforeContent();

        protected override void OnPreEnd()
        {
            OnAfterContent();

            base.OnPreEnd();
        }

        protected abstract void OnAfterContent();


    }
}
