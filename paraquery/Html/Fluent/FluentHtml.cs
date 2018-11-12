using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;
using com.paraquery.Rendering;

namespace com.paraquery.Html.Fluent
{
    /*

        FluentHtml must either be in Block or Inline mode because....

        Since adding HTML elements under an empty elment doesn't make any sense, we don't have
        that concept (like Tag does) and never go into the "Single" LineMode.

    */
    public partial class FluentHtml : HtmlComponent<ParaHtmlPackage>
    {
        public FluentHtml(HtmlContext context, LineModes lineMode, ContainerModes containerMode, bool begin = true) : base(context, lineMode, containerMode, false, false)
        {
            if (begin)
            {
                Begin();
            }
        }

        protected override void OnBegin()
        {
            if (ContainerMode==ContainerModes.Block)
            {
                Push(new HtmlDebugBlock(Context, "fluent html", Context.IsDebug(DebugFlags.FluentHtml), false));
            }
            else
            {
                Push(new HtmlDebugInline(Context, "fluent html", Context.IsDebug(DebugFlags.FluentHtml), false));
            }
        }

        protected override void OnEnd()
        {
        }

        protected override void OnDebug(string text)
        {
            HtmlRenderer.Comment(Writer, text);
        }

        protected new FluentHtml Push(Renderer renderer)
        {
            //this method is just to simplify fluent methods...
            base.Push(renderer);
            return this;
        }

        public new FluentHtml Open(Renderer renderer)
        {
            return Push(renderer);
        }

        public virtual FluentHtml Open<T>(T renderer, Action<T> action) where T : Renderer
        {
            Push(renderer);

            action(renderer);

            return this;
        }

        public FluentHtml CloseUp()
        {
            base.CloseInlines(false);
            return this;
        }

        public FluentHtml CloseBlock()
        {
            base.CloseInlines(true);
            return this;
        }

        public new FluentHtml CloseAll()
        {
            base.CloseAll();
            return this;
        }

        public new FluentHtml Close()
        {
            base.Close();
            return this;
        }

        public new FluentHtml Close(Renderer renderer)
        {
            base.Close(renderer);
            return this;
        }

        public FluentHtml Write(string content)
        {
            Context.Writer.Write(content);
            return this;
        }

        private void CloseUpIfTopNotMultipleLine()
        {
            if (Top?.LineMode != LineModes.Multiple)
            {
                CloseUp();
            }
        }

        public FluentHtml WriteLine(string content)
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.WriteLine(content);
            return this;
        }

        public FluentHtml NewLine()
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.NewLine();
            return this;
        }

        public FluentHtml Space()
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.Space();
            return this;
        }

        public FluentHtml Snippet(string text, string newline = null)
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.Snippet(text, newline);
            return this;
        }

       
    }
}