using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Rendering
{
    public interface IRenderer
    {
        LineModes LineMode { get; }
        ContainerModes ContainerMode { get; }
        RenderStates RenderState { get; }
        void Open(Context context);
        void Close();
    }

    

}
