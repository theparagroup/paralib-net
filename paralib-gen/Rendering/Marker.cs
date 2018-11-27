using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Rendering
{
    public class Marker : IRenderer
    {
        public string Name { protected set; get; }

        public Marker(string name)
        {
            Name = name;
        }

        public ContainerModes ContainerMode
        {
            get
            {
                return ContainerModes.Block;
            }
        }

        public object Data
        {
            get
            {
                return null;
            }
        }

        public LineModes LineMode
        {
            get
            {
                return LineModes.Multiple;
            }
        }

        public void Begin()
        {
        }

        public void End()
        {
        }
    }

    //public class Marker
    //{
    //    public string Name { private set; get; }
    //    public IRenderer Renderer { private set; get; }

    //    public Marker(string name, IRenderer renderer)
    //    {
    //        Name = name;
    //        Renderer = renderer;
    //    }
    //}
}
