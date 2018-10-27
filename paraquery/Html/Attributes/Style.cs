using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;

namespace com.paraquery.Html.Attributes
{
    public class Style:IComplexAttribute
    {
        public string background { get; set; }
        public string backgroundColor { get; set; }
        public Color? BackgroundColor { get; set; }

        public string color { get; set; }
        public Color? Color { get; set; }

        //TODO expand out border class and recursivily hyphenate over class and property
        public string border { get; set; }

        string IComplexAttribute.Value
        {
            get
            {
                var dictionary = new AttributeDictionary();
                TagBuilder.BuildAttributeDictionary(dictionary, this, typeof(Style));
                string style = null;

                if (dictionary.Count > 0)
                {
                    style = string.Join(";", dictionary.Select(e => $"{e.Key}:{e.Value}").ToArray());
                    style = $"{style};";
                }

                return style;
            }
        }
    }
}
