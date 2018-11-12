using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml
{
    public enum FontLengthUnits
    {
        /// <summary>
        /// Represents the "cap height" (nominal height of capital letters) of the element’s font.
        /// </summary>
        //Cap,

        /// <summary>
        /// Represents the width, or more precisely the advance measure, of the glyph "0" (zero, the Unicode character U+0030) in the element's font.
        /// </summary>
        Ch,

        /// <summary>
        /// Represents the calculated font-size of the element. If used on the font-size property itself, it represents the inherited font-size of the element.
        /// </summary>
        Em,

        /// <summary>
        /// Represents the x-height of the element's font. On fonts with the "x" letter, this is generally the height of lowercase letters in the font; 1ex ≈ 0.5em in many fonts.
        /// </summary>
        Ex,

        /// <summary>
        /// Equal to the used advance measure of the "水" (CJK water ideograph, U+6C34) glyph found in the font used to render it.
        /// </summary>
        //Ic,

        /// <summary>
        /// Equal to the computed value of the line-height property of the element on which it is used, converted to an absolute length.
        /// </summary>
        //Lh,

        /// <summary>
        /// Represents the font-size of the root element (typically <html>). When used within the root element font-size, it represents its initial value (a common browser default is 16px, but user-defined preferences may modify this).
        /// </summary>
        Rem,

        /// <summary>
        /// Equal to the computed value of the line-height property on the root element (typically <html>), converted to an absolute length. When used on the font-size or line-height properties of the root element, it refers to the properties' initial value.
        /// </summary>
        //Rlh
    }

}
