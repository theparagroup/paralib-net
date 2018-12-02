using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.parahtml.Html;
using com.paralib.Gen;

namespace com.parahtml.Css
{
    public class Rule : HtmlRenderer
    {
        protected string _selector;

        public Rule(HtmlContext context, string selector) : base(LineModes.Multiple, ContainerModes.None, context.Options.CssFormat!=CssFormats.Readable?false:true)
        {
            ((IRenderer)this).SetContext(context);
            _selector = selector;
        }

        protected override void OnBegin()
        {
            Writer.Space();

            switch (Context.Options.CssFormat)
            {

                case CssFormats.Readable:
                    Writer.WriteLine($"{_selector}");
                    Writer.Write("{");
                    break;

                case CssFormats.Common:
                case CssFormats.Tucked:
                    Writer.WriteLine($"{_selector} {{");
                    break;


                default:
                    throw new InvalidOperationException($"Invalid Css Format {Context.Options.CssFormat}");

            }
        }

        protected override void OnEnd()
        {
            switch (Context.Options.CssFormat)
            {

                case CssFormats.Tucked:
                    Writer.WriteLine($"   }}");
                    break;

                case CssFormats.Common:
                case CssFormats.Readable:
                    Writer.Write("}");
                    break;


                default:
                    throw new InvalidOperationException($"Invalid Css Format {Context.Options.CssFormat}");

            }


        }
    }
}
