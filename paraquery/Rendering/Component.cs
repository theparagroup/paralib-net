using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;

namespace com.paraquery.Rendering
{
    /*
        Provides a base for building custom components with nested content.

        Contains a tagbuilder, and adds OnBeforeContent and OnAfterContent virtual methods.
        
        Note: anything rendered in these before/after content methods will obey the
        visible flag. "Proper content", that is, content rendered outside of the component
        between the Begin() and End() calls, is not controlled by the Component.

    */

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
