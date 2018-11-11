using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html
{
    public enum AngleUnits
    {
        /// <summary>
        /// Represents an angle in degrees. One full circle is 360deg.
        /// </summary>
        Deg,

        /// <summary>
        /// Represents an angle in gradians. One full circle is 400grad.
        /// </summary>
        Grad,

        /// <summary>
        /// Represents an angle in radians. One full circle is 2π radians which approximates to 6.2832rad. 1rad is 180/π degrees.
        /// </summary>
        Rad,

        /// <summary>
        /// Represents an angle in a number of turns. One full circle is 1turn.
        /// </summary>
        Turn
    }
}
