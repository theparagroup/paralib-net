using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;

namespace com.paraquery.Html.Attributes
{
    public class Style : IComplexAttribute
    {
        public string background { get; set; }
        public string backgroundColor { get; set; }
        public Color? BackgroundColor { get; set; }

        public string color { get; set; }
        public Color? Color { get; set; }

        //TODO expand out border class and recursivily hyphenate over class and property
        public string border { get; set; }

        protected string Hyphenate(string name)
        {
            //mixed case should be replaced with hyphens (but not first letter)
            return Regex.Replace(name, @"([a-z])([A-Z])", "$1-$2").ToLower();
        }

        string IComplexAttribute.Value
        {
            get
            {
                var dictionary = new AttributeDictionary();

                //note: we call this in case-sensitive mode so we can hyphenate mixed case
                AttributeDictionary.BuildAttributeDictionary<Style>(dictionary, this, true);

                string style = null;

                if (dictionary.Count > 0)
                {
                    style = string.Join(";", dictionary.Select(e => $"{Hyphenate(e.Key)}:{e.Value}").ToArray());
                    style = $"{style};";
                }

                return style;
            }
        }

    }
}
