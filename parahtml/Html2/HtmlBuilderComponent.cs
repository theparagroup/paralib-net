using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Builders;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.paralib.Gen;

namespace com.parahtml.Html2
{

    public class HtmlBuilderComponent : HtmlBuilder2, IComponent 
    {
        protected IRenderer _start;

        void IComponent.Open(IContainer container)
        {
            ((ILazyContext)this).Initialize(container.Context);

            _start = Open(new Marker(LineModes.Multiple, ContainerModes.Block));
        }


        void IComponent.Close()
        {
            Close(_start);
        }

    }
}
