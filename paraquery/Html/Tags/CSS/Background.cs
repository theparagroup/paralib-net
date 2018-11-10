using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;

namespace com.paraquery.Html
{
    public class Background: StyleBase
    {
        public string backgroundColor { get; set; }
        public Color? BackgroundColor { get; set; }
    }

    public partial class Style 
    {
        public string background { get; set; }
        protected Background _background { get; set; }

        [DynamicValue]
        public Background Background
        {
            set
            {
                _background = value;
            }
            get
            {
                if (_background == null)
                {
                    _background = new Background();
                }

                return _background;
            }
        }


    }

}
