using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html
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
        Page=3,
        FluentHtml=8,



        FluentGrid=16
    }
}
