using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.parahtml.Tags.Fluent;

namespace com.parahtml
{
    /*
        
        A base class for chunks of HTML.

        Page and other HTML components derive from this class.

    */
    public class Fragment : HtmlComponent<ParaHtmlPackage>
    {
        protected Fragment(HtmlContext context, bool visible, bool begin) : base(context, LineModes.Multiple, ContainerModes.Block, visible, false)
        {
            if (begin)
            {
                Begin();
            }
        }

        public Fragment(HtmlContext context, bool begin=true) : base(context, LineModes.Multiple, ContainerModes.Block, context.IsDebug(DebugFlags.Fragment), false)
        {
            if (begin)
            {
                Begin();
            }
        }

        protected override void OnBegin()
        {
            if (Visible)
            {
                Comment("fragment start");
            }
        }

        protected override void OnEnd()
        {
            if (Visible)
            {
                Comment("fragment end");
            }
        }

        protected override void Comment(string text)
        {
            HtmlRenderer.HtmlComment(Writer, text);
        }

        public override string Name
        {
            get
            {
                return "fragment";
            }
        }

        //public FluentCss FluentCss()
        //{
        //    var fluentCss = new FluentCss(Context, this);
        //    Push(fluentCss);
        //    return fluentCss;
        //}

        public Document Document()
        {
            var document = new Document(Context, false);
            Push(document);
            return document;
        }

        public Html Html()
        {
            return new Html(Context, this);
        }

    }
}
