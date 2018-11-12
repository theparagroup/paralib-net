using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Css;
using com.paralib.Gen.NameValuePairs;

namespace com.parahtml
{
    /*
        We're only reifying urls and gradients.

        <image> = <url> | <image()> | <image-set()> | <element()> | <cross-fade()> | <gradient>

        <gradient> = <linear-gradient()> | <repeating-linear-gradient()> | <radial-gradient()> | <repeating-radial-gradient()>

        <color-stop-list> = <color-stop>#{2,}
        <color-stop> = <color> <length-percentage>?
        <length-percentage> = <length> | <percentage>

        <linear-gradient()> = linear-gradient( [ <angle> | to <side-or-corner> ]? , <color-stop-list> )
        <repeating-linear-gradient()> = repeating-linear-gradient( [ <angle> | to <side-or-corner> ]? , <color-stop-list> )
        <side-or-corner> = [ left | right ] || [ top | bottom ]
        
        <radial-gradient()> = radial-gradient( [ <ending-shape> || <size> ]? [ at <position> ]? , <color-stop-list> )
        <repeating-radial-gradient()> = repeating-radial-gradient( [ <ending-shape> || <size> ]? [ at <position> ]? , <color-stop-list> )


    */

    public class BackgroundImage : StyleBase, IValueContainer
    {
        public string[] Urls { set; get; }


        //protected override string GetProperties()
        //{
        //    string style = null;

        //    if (Urls != null)
        //    {
        //        style = string.Join(", ", Urls.Select(url => $"url({url})").ToArray());
        //    }

        //    if (this is Gradient)
        //    {
        //        var gradient = (Gradient)this;

        //        if (style != null)
        //        {
        //            style += ", ";
        //        }

        //        if (gradient.Repeating)
        //        {
        //            style += "repeating-";
        //        }

        //        if (this is LinearGradient)
        //        {
        //            var linearGradient = (LinearGradient)this;

        //            style += "linear-gradient(";

        //            //note degrees and direction cannot both be set (see setters)

        //            if (linearGradient.Angle != null)
        //            {
        //                style += $"{linearGradient.Angle.ToValue()}, ";
        //            }

        //            if (linearGradient.SideOrCorner != null)
        //            {
        //                style += $"to {PropertyDictionary.Spacenate(PropertyDictionary.Lowernate(linearGradient.SideOrCorner))}, ";
        //            }

        //        }
        //        else if (this is RadialGradient)
        //        {
        //            style += "radial-gradient(";


        //            throw new NotImplementedException("radial gradients not implemented");
        //        }


        //        style += $"{gradient.ColorStop1.ToValue()}, {gradient.ColorStop2.ToValue()}";

        //        if (gradient.ColorStops?.Count>0)
        //        {
        //            style += ", ";
        //            style += string.Join(", ", gradient.ColorStops.Select(cs => $"{cs.ToValue()}").ToArray());
        //        }


        //        style += ")";
        //    }


        //    return style;
        //}
    }
}
