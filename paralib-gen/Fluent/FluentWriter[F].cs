using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Fluent
{
    public abstract class FluentWriter<F>where F : FluentWriter<F>
    {
        protected RendererStack RendererStack;
        protected Context Context { set; get; }

        public FluentWriter(RendererStack rendererStack)
        {
            RendererStack = rendererStack;
        }

        private void CloseUpIfTopNotMultipleLine()
        {
            if (RendererStack.Top?.LineMode != LineModes.Multiple)
            {
                RendererStack.CloseUp();
            }
        }

        public F Write(string content)
        {
            Context.Writer.Write(content);
            return (F)this;
        }

        public F WriteLine(string content)
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.WriteLine(content);
            return (F)this;
        }

        public F NewLine()
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.NewLine();
            return (F)this;
        }

        public F Space()
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.Space();
            return (F)this;
        }

        public F Snippet(string text, string newline = null)
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.Snippet(text, newline);
            return (F)this;
        }

        public F Indent()
        {
            Context.Writer.Indent();
            return (F)this;
        }

        public F Dedent()
        {
            Context.Writer.Dedent();
            return (F)this;
        }

    }
}
