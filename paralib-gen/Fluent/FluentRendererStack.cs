using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Fluent
{
    public class FluentRendererStack<C> : FluentRendererStack<C, FluentRendererStack<C>> where C:Context
    {
        public FluentRendererStack(C context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }
    }
}
