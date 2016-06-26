using System;
using System.Reflection;
using com.paralib.Logging;

namespace com.paralib
{
    public partial class Paralib
    {

        public static DataAnnotations.ParaTypes ParaTypes
        {
            get
            {
                return DataAnnotations.ParaTypes.Instance;
            }
        }

    }
}
