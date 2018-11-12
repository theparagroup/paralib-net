using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml
{
    public enum ViewPortLengthUnits
    {
        /// <summary>
        /// Equal to 1% of the height of the viewport's initial containing block.
        /// </summary>
        Vh,

        /// <summary>
        /// Equal to 1% of the width of the viewport's initial containing block.
        /// </summary>
        Vw,

        /// <summary>
        /// Equal to 1% of the size of the initial containing block, in the direction of the root element’s inline axis.
        /// </summary>
        //Vi,

        /// <summary>
        /// Equal to 1% of the size of the initial containing block, in the direction of the root element’s block axis.
        /// </summary>
        //Vb,

        /// <summary>
        /// Equal to the smaller of vw and vh.
        /// </summary>
        Vmin,

        /// <summary>
        /// Equal to the larger of vw and vh.
        /// </summary>
        Vmax
    }


}
