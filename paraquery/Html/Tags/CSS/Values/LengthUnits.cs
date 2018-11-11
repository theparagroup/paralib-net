using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html
{
    public enum LengthUnits
    {
        /// <summary>
        /// One pixel. For screen displays, it traditionally represents one device pixel (dot). However, for printers and high-resolution screens, one CSS pixel implies multiple device pixels. 1px = 1/96th of 1in.
        /// </summary>
        Px,

        /// <summary>
        /// One centimeter. 1cm = 96px/2.54.
        /// </summary>
        Cm,

        /// <summary>
        /// One millimeter. 1mm = 1/10th of 1cm.
        /// </summary>
        Mm,

        /// <summary>
        /// One quarter of a millimeter. 1Q = 1/40th of 1cm.
        /// </summary>
        Q,

        /// <summary>
        /// One inch. 1in = 2.54cm = 96px.
        /// </summary>
        In,

        /// <summary>
        /// One pica. 1pc = 12pt = 1/6th of 1in.
        /// </summary>
        Pc,

        /// <summary>
        /// One point. 1pt = 1/72nd of 1in.
        /// </summary>
        Pt
    }

}
