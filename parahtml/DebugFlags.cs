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
        EndTag =1 

    }
}
