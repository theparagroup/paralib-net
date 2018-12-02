using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Rendering
{
    public sealed class Renderer : RendererBase
    {
        /*

            This is a concrete (sealed) implementation of RendererBase that can be used in
            object composition designs to bring in the formatting logic for classes that
            implement IRenderer.


            Usage:

                void IRenderer.Begin()
                {
                    _renderer.OnPreBegin();
                    Writer.Write("begining");
                    _renderer.OnPostBegin();
                }

                void IRenderer.End()
                {
                    _renderer.OnPreEnd();
                    Writer.Write("ending");
                    _renderer.OnPostEnd();
                }



        */
        public Renderer(Context context, LineModes lineMode, ContainerModes containerMode, bool indentContent) : base(lineMode, containerMode, indentContent)
        {
            Context = context;
        }

        public new void OnPreBegin()
        {
            base.OnPreBegin();
        }

        protected override void OnBegin()
        {
        }

        public new void OnPostBegin()
        {
            base.OnPostBegin();
        }

        public new void OnPreEnd()
        {
            base.OnPreEnd();
        }

        protected override void OnEnd()
        {
        }

        public new void OnPostEnd()
        {
            base.OnPostEnd();
        }
    }
}
