﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;

namespace com.paraquery.Html.Tags
{
    public class BlockTag : BlockRenderer, ITag, ICommentator
    {
        protected Tag _tag;

        public BlockTag(TagBuilder tagBuilder, string tagName, object attributes, bool empty = false) : base(tagBuilder.Context, !empty)
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
