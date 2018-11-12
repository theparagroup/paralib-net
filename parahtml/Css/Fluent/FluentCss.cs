using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Tags.Fluent;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Css.Fluent
{
    //this doesn't need to be a component

    public class FluentCss : HtmlRenderer, IFluentCss
    {
        protected FluentHtml _fluentHtml;

        public FluentCss(HtmlContext context, FluentHtml fluentHtml) : base(context, LineModes.Multiple, ContainerModes.Block, context.IsDebug(DebugFlags.FluentCss), false)
        {
            _fluentHtml = fluentHtml;
        }

        protected override void Comment(string text)
        {
            HtmlRenderer.CssComment(Writer, text);
        }

        protected override void OnBegin()
        {
            if (Visible)
            {
                Comment("fluent css start");
            }
        }

        protected override void OnEnd()
        {
            Writer.Space();

            if (Visible)
            {
                Comment("fluent css end");
            }
        }

        public IFluentCss Rule(string selector)
        {
            _fluentHtml.Open(new Rule(Context, selector));
            return this;
        }

        public IFluentCss Rule(Action<ISelectorLevel> selector)
        {
            return this;
        }

        public IFluentCss Declaration(string declaration)
        {
            if (declaration != null)
            {
                if (Context.Options.CssFormat != CssFormats.Readable)
                {
                    Writer.Write("   ");
                }

                Writer.WriteLine(declaration);
            }

            return this;
        }

        public IFluentCss Declaration(Action<Style> declaration)
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

        public FluentHtml FluentHtml()
        {
            return _fluentHtml.Close(this);
        }

        public void Close()
        {
            _fluentHtml.Close(this);
        }

    }


}
