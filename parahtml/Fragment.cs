using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.parahtml.Tags.Fluent;
using com.paralib.Gen.Fluent;

namespace com.parahtml
{
    /*
        
        A base class for chunks of HTML.

        Page and other HTML components derive from this class.

    */
    public class Fragment : HtmlComponent<ParaHtmlPackage>
    {
        protected FluentWriter<HtmlContext> _fluentWriter;

       

        protected Fragment(HtmlContext context, bool visible, bool begin) : base(context, LineModes.Multiple, ContainerModes.Block, visible, false)
        {
            _fluentWriter = new FluentWriter<HtmlContext>(context, this);

            if (begin)
            {
                Begin();
            }
        }

        public Fragment(HtmlContext context, bool begin=true) : base(context, LineModes.Multiple, ContainerModes.Block, context.IsDebug(DebugFlags.Fragment), false)
        {
            _fluentWriter = new FluentWriter<HtmlContext>(context, this);

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

        public IDocument Document()
        {
            return new Document(Context, this);
        }

        public Html Html()
        {
            return new Html(Context, this);
        }

        public Fragment Write(string content)
        {
            _fluentWriter.Write(content);
            return this;
        }

        public Fragment WriteLine(string content)
        {
            _fluentWriter.WriteLine(content);
            return this;
        }

        public Fragment NewLine()
        {
            _fluentWriter.NewLine();
            return this;
        }

        public Fragment Space()
        {
            _fluentWriter.Space();
            return this;
        }

        public Fragment Snippet(string text, string newline = null)
        {
            _fluentWriter.Snippet(text, newline);
            return this;
        }
    }
}
