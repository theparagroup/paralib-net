using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;

namespace com.parahtml.Css
{
    /*
        
        https://www.w3.org/TR/css-backgrounds-3/
        https://developer.mozilla.org/en-US/docs/Web/CSS/background
        http://htmldog.com/references/css/properties/background/

        
        background:
            attachment
            box
            background-color
            bg-image
            position
            repeat-style
            bg-size

        Supports multiple layers:


        #example1 
        {
            background-image: url(img_flwr.gif), url(paper.gif);
            background-position: right bottom, left top;
            background-repeat: no-repeat, repeat;
        } 

        #example2 
        {
            background: url(img_flwr.gif) right bottom no-repeat, url(paper.gif) left top repeat;
        } 


    */

    [Prefix]
    public class Background: StyleBase 
    {
        public Color? Color { set; get; }
        public string color { set; get; }

        [DynamicValue]
        public BackgroundImage Image => _get<BackgroundImage>(nameof(Image));
        public string image { set; get; }






    }


}
