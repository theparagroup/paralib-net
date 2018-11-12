using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml
{
    [Flags]
    public enum DebugFlags
    {
        //required for testing absence of flags
        None=0,

        //endings of blocks with ids
        EndTag =1, 

        //see the begin and end of various components
        Fragment = 2,
        Page=4,
        FluentHtml=8,
        FluentCss = 16,



        FluentGrid = 32
    }
}
