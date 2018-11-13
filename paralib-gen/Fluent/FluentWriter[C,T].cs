using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Fluent
{
    public class FluentWriter<C, T>:FluentWriter<C>, IFluentWriter<T> where C: Context where T : FluentWriter<C, T>
    {
        public FluentWriter(C context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }

        public new T Write(string content)
        {
            base.Write(content);
            return (T)this;
        }

        public new T WriteLine(string content)
        {
            base.WriteLine(content);
            return (T)this;
        }

        public new T NewLine()
        {
            base.NewLine();
            return (T)this;
        }

        public new T Space()
        {
            base.Space();
            return (T)this;
        }

        public new T Snippet(string text, string newline = null)
        {
            base.Snippet(text, newline);
            return (T)this;
        }
    }
}
