using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;
using com.parahtml.Packages;

namespace com.parahtml.Core
{
    /*
        HtmlComponent is a base class for "components" that are HTML-centric. It requires an
        HtmlContext, defines OnDebug, etc.
        
        HtmlComponent follows the intended "component pattern", that is, a class derived from
        RendererStack that builds its own content.

        Because of the rules RendererStack enforces about the first renderer in the stack,
        HtmlComponent declares itself to be Multiple/Nested and invisible, and requires
        derived classes to provide the first renderer to go on the stack (start renderer).
        
        If implementors derive from HtmlComponent, they don't have to worry about these details. 

    */
    public abstract class HtmlComponent<T> : RendererStack where T : Package, new()
    {

        public HtmlComponent(HtmlContext context, LineModes lineMode, ContainerModes containerMode, bool visible, bool indentContent) : base(context, lineMode, containerMode, visible, indentContent)
        {
            //register package
            context.RegisterPackage<T>();
        }

        protected new HtmlContext Context
        {
            get
            {
                return (HtmlContext)base.Context;
            }
        }

        protected HtmlBuilder HtmlBuilder
        {
            get
            {
                return Context.HtmlBuilder;
            }
        }


        protected override void OnDebug(string text)
        {
            HtmlRenderer.Comment(Writer, text);
        }

    }
}
