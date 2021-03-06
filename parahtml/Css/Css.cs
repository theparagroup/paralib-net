﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.paralib.Gen.Fluent;
using com.parahtml.Html;

namespace com.parahtml.Css
{

    public class FluentCss : FluentRendererStack<FluentCss>, ICss
    {
        protected HtmlContext Context;

        public FluentCss(HtmlContext context, ContentStack contentStack) : base(contentStack)
        {
            Context = context;
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
            //if (declaration != null)
            //{
            //    var style = new Style();

            //    declaration(style);

            //    var properties = Context.PropertyBuilder.Properties(style);

            //    var declarations = Context.PropertyBuilder.ToList(properties);

            //    foreach (var decl in declarations)
            //    {
            //        Declaration($"{decl};");
            //    }

            //}



            return this;
        }



    }


}
