using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Fluent;
using com.paraquery.Html;
using com.paraquery.Rendering;

namespace com.paraquery.Html.Tags.CSS.Fluent
{
    //this doesn't need to be a component

    public class FluentCss : CssBlock, IFluentCss
    {
        protected FluentHtml _fluentHtml;

        public FluentCss(HtmlContext context, FluentHtml fluentHtml) : base(context, "fluent css", context.IsDebug(DebugFlags.FluentCss), false)
        {
            _fluentHtml = fluentHtml;
        }

        protected override void OnBegin()
        {
            base.OnBegin();
        }

        protected override void OnEnd()
        {
            Writer.NewLine();
            base.OnEnd();
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

                var properties = PropertyDictionary.Properties(style);

                var declarations = properties.ToDeclarations();

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

        public new void Close()
        {
            _fluentHtml.Close(this);
        }

    }


}
