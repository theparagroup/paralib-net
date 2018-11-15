using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;
using com.parahtml.Packages;
using com.parahtml.Core;

namespace com.parahtml.Html
{
    /*
        HtmlComponentBase is for components that require a package, that is,

            Scripts
            Style Sheets
            Namespaces
            etc.





    */
    public abstract class HtmlComponentBase<F, P> : FluentHtmlBase<F> where F : HtmlComponentBase<F,P>  where P : Package, new()
    {
        public HtmlComponentBase(HtmlContext context, RendererStack rendererStack) : base(context, rendererStack)
        {
            //register package
            context.RegisterPackage<P>();
        }


    }
}
