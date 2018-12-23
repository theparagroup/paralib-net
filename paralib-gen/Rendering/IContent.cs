using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Rendering
{
    public interface IContent:ICloseable
    {
        LineModes LineMode { get; }
        ContainerModes ContainerMode { get; }
        ContentStates ContentState { get; }
        void Open(Context context, LineModes? lineMode);
        void Close();
    }

    

}
