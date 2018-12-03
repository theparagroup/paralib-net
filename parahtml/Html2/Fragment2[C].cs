using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.paralib.Gen.Builders;

namespace com.parahtml.Html2
{
    public class Fragment2<C>:HtmlBuilder2<C>, IDisposable where C : HtmlContext
    {
        public Fragment2(RendererStack rendererStack) : base(rendererStack)
        {
        }

        public void Dispose()
        {
            ((IBuilderBase<C>)this).CloseAll();
        }
    }
}
