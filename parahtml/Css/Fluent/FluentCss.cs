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
        protected Html _html;

        public FluentCss(HtmlContext context, Html html) : base(context, LineModes.Multiple, ContainerModes.Block, context.IsDebug(DebugFlags.FluentCss), false)
        {
            _html = html;
        }

        protected override void Comment(string text)
        {
            CssComment(Writer, text);
        }

        public override string Name
        {
            get
            {
                return "fluent css";
            }
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
            _html.Open(new Rule(Context, selector));
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

        public Html Html()
        {
            return _html.Close(this);
        }

        public void Close()
        {
            _html.Close(this);
        }

    }


}
