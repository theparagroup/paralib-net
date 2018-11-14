using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Tags.Fluent;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.paralib.Gen.Fluent;

namespace com.parahtml.Css.Fluent
{

    public class Css : FluentRendererStack<HtmlContext, Css>, ICss
    {
        public Css(CssContext context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }

        public ICss Rule(string selector)
        {
            Open(new Rule(Context, selector));
            return this;
        }

        public ICss Rule(Action<ISelectorLevel> selector)
        {
            return this;
        }

        public ICss Declaration(string declaration)
        {
            if (declaration != null)
            {
                if (Context.Options.CssFormat != CssFormats.Readable)
                {
                    Write("   ");
                }

                WriteLine(declaration);
            }

            return this;
        }

        public ICss Declaration(Action<Style> declaration)
        {
            if (declaration != null)
            {
                var style = new Style();

                declaration(style);

                var properties = Context.PropertyBuilder.Properties(style);

                var declarations = Context.PropertyBuilder.ToList(properties);

                foreach (var decl in declarations)
                {
                    Declaration($"{decl};");
                }

            }



            return this;
        }



    }


}
