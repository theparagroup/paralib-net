using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Rendering
{
    public class Marker : IContent
    {
        protected LineModes _lineMode { private set; get; }
        protected ContainerModes _containerMode { private set; get; }
        protected ContentStates _contentState { private set; get; } = ContentStates.New;

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

        public ContentStates ContentState
        {
            get
            {
                return _contentState;
            }
        }

        public void Open(Context context, LineModes? lineMode)
        {
            _contentState = ContentStates.Open;
        }

        public void Close()
        {
            _contentState = ContentStates.Closed;
        }

    }


}
