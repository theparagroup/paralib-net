using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.common
{
    public class ParalibException: Exception
    {
        public ParalibException(string message):base(message)
        {

        }

        public ParalibException(string message, Exception innerException) : base(message,innerException)
        {

        }
    }
}
