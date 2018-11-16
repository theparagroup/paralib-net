using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Rendering
{
    public class Marker
    {
        public string Name { private set; get; }
        public IRenderer Renderer { private set; get; }

        public Marker(string name, IRenderer renderer)
        {
            Name = name;
            Renderer = renderer;
        }
    }
}
