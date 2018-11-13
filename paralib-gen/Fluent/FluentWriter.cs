using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Fluent
{
    public class FluentWriter<C>:IFluentWriter where C: Context
    {
        protected RendererStack _rendererStack;
        protected C Context { private set; get; }

        public FluentWriter(C context, RendererStack rendererStack)
        {
            _rendererStack = rendererStack;
            Context = context;
        }

        private void CloseUpIfTopNotMultipleLine()
        {
            if (_rendererStack.Top?.LineMode != LineModes.Multiple)
            {
                _rendererStack.CloseUp();
            }
        }

        public void Write(string content)
        {
            Context.Writer.Write(content);
        }

        public void WriteLine(string content)
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.WriteLine(content);
        }

        public void NewLine()
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.NewLine();
        }

        public void Space()
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.Space();
        }

        public void Snippet(string text, string newline = null)
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.Snippet(text, newline);
        }
    }
}
