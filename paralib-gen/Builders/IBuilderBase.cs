using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Builders
{
    public interface IBuilderBase<C> where C:Context
    {
        void SetContext(C context);
        void Open(IRenderer renderer);
        void CloseAll();
    }
}
