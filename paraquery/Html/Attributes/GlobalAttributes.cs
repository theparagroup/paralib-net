using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Attributes
{
    public class GlobalAttributes
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public int? TabIndex { get; set; }
        public string Title { get; set; }

        //TODO provide ISO codes
        public string Lang { get; set; }

        //TODO this is a whole thing
        //public string Style { get; set; }
        protected Style _style;
        public Style Style
        {
            get
            {
                if (_style==null)
                {
                    _style = new Style();
                }

                return _style;
            }
        }

        public object Additional { get; set; }

    }
}
