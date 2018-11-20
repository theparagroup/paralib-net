using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Css;
using com.paralib.Gen.Mapping;
using com.parahtml.Core;

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

    public class BackgroundImage : IComplexValue<HtmlContext>
    {
        protected List<object> _images=new List<object>();

        string IComplexValue<HtmlContext>.ToValue(HtmlContext context)
        {
            string value = null;

            foreach (var image in _images)
            {
                if (value != null)
                {
                    value += ", ";
                }

                if (image is string)
                {
                    value += image;
                }
                else if (image is Url)
                {
                    value += ((Url)image).ToValue(context);
                }

            }

            return value;
        }

        public void None()
        {
            _images.Add("none");
        }

        public void Url(string url)
        {
            _images.Add(new Url(url));
        }

        protected void Gradient(string name, string stuff, ColorStop colorStop1, ColorStop colorStop2, ColorStop[] colorStops, bool repeating)
        {
            string extraStops = null;

            if (colorStops!=null)
            {
                foreach (var colorStop in colorStops)
                {
                    extraStops += ", ";
                    extraStops += colorStop.ToString();
                }
            }

            if (stuff!=null)
            {
                _images.Add($"{(repeating?"repeating-":"")}{name}({stuff}, {colorStop1}, {colorStop2}{extraStops})");

            }
            else
            {
                _images.Add($"{(repeating ? "repeating-" : "")}{name}({colorStop1}, {colorStop2}{extraStops})");
            }

        }

        public void LinearGradient(ColorStop colorStop1, ColorStop colorStop2, ColorStop[] colorStops = null, bool repeating=false)
        {
            Gradient("linear-gradient", null, colorStop1, colorStop2, colorStops, repeating);
        }

        public void LinearGradient(Angle angle, ColorStop colorStop1, ColorStop colorStop2, ColorStop[] colorStops=null, bool repeating = false)
        {
            Gradient("linear-gradient", angle.ToString(), colorStop1, colorStop2, colorStops, repeating);
        }

        public void LinearGradient(SideOrCorner sideOrCorner, ColorStop colorStop1, ColorStop colorStop2, ColorStop[] colorStops = null, bool repeating = false)
        {
            Gradient("linear-gradient", HtmlBuilder.SpacenateMixedCase(sideOrCorner.ToString()), colorStop1, colorStop2, colorStops, repeating);
        }

        public void RadialGradient(ColorStop colorStop1, ColorStop colorStop2, ColorStop[] colorStops = null, bool repeating = false)
        {
            Gradient("radial-gradient", null, colorStop1, colorStop2, colorStops, repeating);
        }

        public void RadialGradient(Action<RadialGradientOptions> options, ColorStop colorStop1, ColorStop colorStop2, ColorStop[] colorStops = null, bool repeating = false)
        {
            string stuff = null;

            if (options!=null)
            {
                var o = new RadialGradientOptions();
                options(o);
                stuff = o.ToString();
            }

            Gradient("radial-gradient", stuff, colorStop1, colorStop2, colorStops, repeating);
        }




    }
}
