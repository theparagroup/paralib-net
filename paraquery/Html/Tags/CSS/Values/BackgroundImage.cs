using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;

namespace com.paraquery.Html
{
    public class BackgroundImage:StyleBase
    {
        public string[] Urls { set; get; }


        protected override string GetProperties()
        {
            string style = null;

            if (Urls != null)
            {
                style = string.Join(", ", Urls.Select(url => $"url({url})").ToArray());
            }

            if (this is Gradient)
            {
                var gradient = (Gradient)this;

                if (style != null)
                {
                    style += ", ";
                }

                if (gradient.Repeating)
                {
                    style += "repeating-";
                }

                if (this is LinearGradient)
                {
                    var linearGradient = (LinearGradient)this;

                    style += "linear-gradient(";

                    //note degrees and direction cannot both be set (see setters)

                    if (linearGradient.Degrees != null)
                    {
                        style += $"{linearGradient.Degrees.ToString()}deg, ";
                    }

                    if (linearGradient.Direction != null)
                    {
                        style += $"to {Spacenate(linearGradient.Direction.ToString()).ToLower()}, ";
                    }

                }
                else if(this is RadialGradient)
                {
                    style += "radial-gradient(";


                    throw new NotImplementedException("radial gradients");
                }

                if (gradient.ColorStops?.Count>=2)
                {
                    style += string.Join(", ", gradient.ColorStops.Select(cs => $"{((IComplexAttribute)cs).Value}").ToArray());
                }
                else
                {
                    throw new InvalidOperationException("CSS Gradients require at least two color stops");
                }

                style += ")";
            }


            return style;
        }
    }
}
