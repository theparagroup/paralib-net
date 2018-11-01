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

    public abstract class Component : RendererStack
    {
        public Component(Context context, RenderModes renderModes, bool visible) : base(context, renderModes, visible)
        {
        }

        protected override void OnPostBegin()
        {
            base.OnPostBegin();

            OnPreContent();
        }

        protected virtual void OnPreContent()
        {

        }

        protected override void OnPreEnd()
        {
            OnPostContent();

            base.OnPreEnd();
        }

        protected virtual void OnPostContent()
        {

        }


    }
}
