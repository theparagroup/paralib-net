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
        protected Renderer _start;

        public Component(Context context, Renderer renderer) : base(context, FormatModes.None, renderer.StructureMode)
        {
            _start = renderer;
        }

        protected override void DoBegin()
        {
            OnPreBegin();
            Push(_start);
            OnBegin();
            OnPostBegin();
            OnPreContent();
        }

        internal override void DoDispose()
        {
            if (_begun)
            {
                base.DoDispose();
            }
            else
            {
                throw new InvalidOperationException($"Can't End() Component '{GetType().Name}' without Begin()");
            }
        }

    }
}
