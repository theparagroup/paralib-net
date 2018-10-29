using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;
using com.paraquery.Rendering;

namespace com.paraquery.Html.Tags
{
    /* 

        This is a utility class that Block and Inline can use to delegate functionality.

        We need to do this because Inline derives from Renderer, and Block derives from BlockRenderer.

        Note on empty tags (also called void tags in HTML5 and self-closing tags in XHTML):

            While you can have self-closing tags like <div /> in XHTML, we don't do that.

            HTML4/XHTML empty/void elements:
                area
                base
                br
                col
                hr
                img
                input
                link
                meta
                param

            Additional HTML5 void elements
                keygen
                source
                track
                embed
                wbr
                menuitem (removed)
                command (obsololete)
            
        The close tag " />" is optional for void tags in HTML.

        TagBuilder uses the SelfClosingTags option to control how the void tags are formatted.

    */


    public class Tag : HtmlRenderer
    {
        protected TagBuilder _tagBuilder;
        protected string _tagName;
        protected bool _empty;
        protected AttributeDictionary _attributes;

        public Tag(TagBuilder tagBuilder, string tagName, bool block, bool empty, AttributeDictionary attributes):base(tagBuilder.Context, GetRenderMode(block, empty))
        {
            _tagBuilder = tagBuilder;
            _tagName = tagName;
            _empty = empty;
            _attributes = attributes;
        }

        private static RenderModes GetRenderMode(bool block, bool empty)
        {
            if (block)
            {
                if (empty)
                {
                    return RenderModes.Line;
                }
                else
                {
                    return RenderModes.Block;
                }
            }
            else
            {
                return RenderModes.Inline;
            }

        }

        protected override void Debug(string text)
        {
            base.Debug($"{text} {_tagName} {_attributes?["id"]}");
        }

        protected override void OnBegin()
        {
            _tagBuilder.Open(_tagName, _empty, _attributes);
        }

        protected override void OnEnd()
        {
            _tagBuilder.Close(_tagName, _empty);
        }

    }
}
