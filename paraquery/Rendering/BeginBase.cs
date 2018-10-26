using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{

    /*

       Base class for "Renderers", the general conceptual unit paraquery is built around.

       Provides for context and a writer, etc.

       Defines the basics of the IDisposable interface ("End" semantics). It is up to the instatiator wrap 
       the instance in a using() statement, or to call End() explicitly.

       Also provides "Begin semantics". It is up to the instatiator or subclass to call Begin().

       Renderer derives from RendererBase, and is the primary class used in the framework, but whenever an implementor
       wants to use "using()" syntax, they can derive from RendererBase.

   */

    public abstract class BeginBase : EndBase
    {

        public void Begin()
        {
            _begin();
        }

        protected virtual void _begin()
        {
            OnPreBegin();
            OnBegin();
            OnPostBegin();
        }

        protected virtual void OnPreBegin() { }

        protected abstract void OnBegin();

        protected virtual void OnPostBegin() { }


    }
}
