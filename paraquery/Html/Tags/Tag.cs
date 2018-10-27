using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    public class Tag
    {
        public TagBuilder TagBuilder { protected set; get; }
        public string TagName { protected set; get; }
        public bool Empty { protected set; get; } 
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
