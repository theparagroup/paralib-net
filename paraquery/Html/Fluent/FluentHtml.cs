using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;
using com.paraquery.Rendering;

namespace com.paraquery.Html.Fluent
{
    public partial class FluentHtml : HtmlComponent<ParaHtmlPackage>
    {

        public FluentHtml(HtmlContext context, bool begin=true) : base(context, new HtmlBlock(context, "fluent html", context.IsDebug(DebugFlags.FluentHtml), false))
        {
            if (begin)
            {
                Begin();
            }
        }

        protected override void OnBegin()
        {
        }

        protected override void OnEnd()
        {
            CloseAll();
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
            base.CloseLinears(false);
            return this;
        }

        public FluentHtml CloseBlock()
        {
            base.CloseLinears(true);
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

        private void CloseUpIfTopIsLinear()
        {
            if (Top?.StackMode == StackModes.Linear)
            {
                CloseUp();
            }
        }

        public FluentHtml WriteLine(string content)
        {
            CloseUpIfTopIsLinear();
            Context.Writer.WriteLine(content);
            return this;
        }

        public FluentHtml NewLine()
        {
            CloseUpIfTopIsLinear();
            Context.Writer.NewLine();
            return this;
        }

        public FluentHtml Space()
        {
            CloseUpIfTopIsLinear();
            Context.Writer.Space();
            return this;
        }

        public FluentHtml Snippet(string text, string newline = null)
        {
            CloseUpIfTopIsLinear();
            Context.Writer.Snippet(text, newline);
            return this;
        }

       
    }
}