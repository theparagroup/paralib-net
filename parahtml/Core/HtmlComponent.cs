﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;
using com.parahtml.Packages;
using com.paralib.Gen.Fluent;

namespace com.parahtml.Core
{
    /*
        HtmlComponent is a base class for "components" that are HTML-centric. It requires an
        HtmlContext, etc.
    */
    public abstract class HtmlComponent<F, P> : FluentRendererStack<HtmlContext, F>, IFluentRendererStack<F> where F : HtmlComponent<F,P>  where P : Package, new()
    {
        public HtmlComponent(HtmlContext context, RendererStack rendererStack) : base(context, rendererStack)
        {
            //register package
            context.RegisterPackage<P>();
        }

        public HtmlBuilder HtmlBuilder
        {
            get
            {
                return Context.HtmlBuilder;
            }
        }

    }
}
