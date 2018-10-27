using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;

namespace com.paraquery.Html.Tags
{
    public class InlineTag : Renderer, ITag, ICommentator
    {
        protected Tag _tag;

        public InlineTag(TagBuilder tagBuilder, string tagName, AttributeDictionary attributes, bool empty = false) : base(tagBuilder.Context)
        {
            _tag = new Tag(tagBuilder, tagName, attributes, empty);
        }

        public void Comment(string text)
        {
            _tag.Comment(text);
        }

        protected override void OnBegin()
        {
            _tag.OnBegin();
        }

        protected override void OnEnd()
        {
            _tag.OnEnd();
        }
    }
}
