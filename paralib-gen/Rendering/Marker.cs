using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Rendering
{
    public class Marker : IRenderer
    {
        protected LineModes _lineMode { private set; get; }
        protected ContainerModes _containerMode { private set; get; }
        protected RenderStates _renderState { private set; get; } = RenderStates.New;

        public Marker(LineModes lineMode, ContainerModes containerMode)
        {
            _lineMode = lineMode;
            _containerMode = containerMode;
        }

        public ContainerModes ContainerMode
        {
            get
            {
                return _containerMode;
            }
        }

        public LineModes LineMode
        {
            get
            {
                return _lineMode;
            }
        }

        public RenderStates RenderState
        {
            get
            {
                return _renderState;
            }
        }

        public void Open(Context context)
        {
            _renderState = RenderStates.Open;
        }

        public void Close()
        {
            _renderState = RenderStates.Closed;
        }

    }


}
