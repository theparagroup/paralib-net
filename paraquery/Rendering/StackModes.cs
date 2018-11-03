using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{
    /*
        Just as you can't put block elements inside inline elements in HTML,
        you can't put structured renderers inside non-structured renderers
        in the RendererStack. The 

    */

    public enum StackModes //StructureModes
    {
        Inline, 
        Line,
        Block 
    }
}
