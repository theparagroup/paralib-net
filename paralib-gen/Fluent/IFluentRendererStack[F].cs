using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Fluent
{
    public interface IFluentRendererStack<F> : IFluentWriter<F> where F : class
    {
        IRenderer Top { get; }
        F Open(IRenderer renderer);
        F CloseUp();
        F CloseBlock();
        F CloseAll();
        F Close();
        F Close(IRenderer renderer);
        F Open<R>(R renderer, Action<R> action) where R : IRenderer;
        F Here(Action<F> action);
    }
}
