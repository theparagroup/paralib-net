using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Tags
{
    public class Tag
    {
        public TagBuilder TagBuilder { protected set; get; }
        public string TagName { protected set; get; }
        public bool Empty { protected set; get; } //i.e., void element (in html5 the / is optional), self-closing xml tag
        public object Attributes { protected set; get; }

        public Tag(TagBuilder tagBuilder, string tagName, object attributes, bool empty = false)
        {
            TagBuilder = tagBuilder;
            TagName = tagName;
            Empty = empty;
            Attributes = attributes;
        }

        public void Comment(string text)
        {
            TagBuilder.Context.Writer.Write($" <!-- {text} -->");
        }

        public void OnBegin()
        {
            if (Empty)
            {
                TagBuilder.Empty(TagName, Attributes);
            }
            else
            {
                TagBuilder.Open(TagName, Attributes);
            }
        }

        public void OnEnd()
        {
            if (!Empty)
            {
                TagBuilder.Close(TagName);
            }
        }


    }
}
