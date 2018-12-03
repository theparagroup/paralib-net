using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Builders
{

    public class Closable: IClosable
    {
        protected IRenderer _renderer;

        public Closable(IRenderer renderer)
        {
            if (renderer.ContainerMode!=ContainerModes.Block)
            {
                throw new InvalidOperationException("Closables must have a container mode of 'Block'");
            }

            _renderer = renderer;
        }


        public IRenderer Renderer
        {
            get
            {
                return _renderer;
            }
            
        }

    }
}
