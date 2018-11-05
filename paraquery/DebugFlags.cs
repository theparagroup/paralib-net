using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    [Flags]
    public enum DebugFlags
    {
        //required for testing absence of flags
        None=0,

        //injected newlines
        SourceFormatting = 1, 

        //endings of blocks with ids
        EndTag =2, 

        //see the begin and end of various components
        Fragment = 4,
        Page=8,
        FluentHtml=16,
        FluentGrid=32
    }
}
