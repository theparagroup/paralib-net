using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Fluent
{
    public interface IFluentStack<T> : IFluentWriter<T>
    {
        T Open(Renderer renderer);
        T CloseUp();
        T CloseBlock();
        T CloseAll();
        T Close();
        T Close(Renderer renderer);
        T Open<R>(R renderer, Action<R> action) where R : Renderer;
        T Here(Action<T> action);
    }
}
