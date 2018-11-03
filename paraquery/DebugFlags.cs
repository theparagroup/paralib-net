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
        None=0,
        SourceFormatting=1,
        EndTag=2,
        Fragment=4,
        Page=8,
        FluentHtml=16,
        FluentGrid=32
    }
}
