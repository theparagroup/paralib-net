using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Tags
{
    /* 

        This is a utility class that converts HTML Block/Inline+Empty concepts to RendererStack
        concepts.

        GetLineMode() below basically makes some hard-coded formatting choices for our HTML:

            Non-empty Block tags are Multiple and the content is indented
            Empty Block tags are Single
            Inline (empty or not) tags are always None

            Example:

                <block>
                    <span><span>content</span></span>
                    <hr />
                </block>

        Notes on empty tags (also called void tags in HTML5 and self-closing tags in XHTML):

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

        HtmlBuilder uses the SelfClosingTags option to control how the void tags are formatted.

    */



    public class Tag : HtmlRenderer
    {
        public string TagName { private set; get; }
        public TagTypes TagType { private set; get; }
        public AttributeDictionary Attributes { private set; get; }

        public Tag(HtmlContext context, TagTypes tagType, string tagName, AttributeDictionary attributes, bool empty = false) : base(context, GetLineMode(tagType, empty), GetContainerMode(tagType, empty), true, true)
        {
            TagName = tagName;
            Attributes = attributes;
        }

        public bool Empty
        {
            get
            {
                return ContainerMode==ContainerModes.None;
            }
        }


        private static LineModes GetLineMode(TagTypes tagType, bool empty)
        {
            if (tagType == TagTypes.Block)
            {
                if (empty)
                {
                    return LineModes.Single;
                }
                else
                {
                    return LineModes.Multiple;
                }
            }
            else
            {
                return LineModes.None;
            }

        }

        private static ContainerModes GetContainerMode(TagTypes tagType, bool empty)
        {
            if(empty)
            {
                return ContainerModes.None;
            }
            else
            {
                if (tagType == TagTypes.Block)
                {
                    return ContainerModes.Block;
                }
                else
                {
                    return ContainerModes.Inline;
                }
            }
        }

        protected override void Comment(string text)
        {
            HtmlRenderer.HtmlComment(Writer, text);
        }

        protected override void OnBegin()
        {
            Writer.Write($"<{TagName}");

            if (Attributes != null)
            {
                var attributes = Attributes.ToAttributesString(Context.Options.MinimizeBooleans, Context.Options.EscapeAttributeValues);
                Writer.Write($" ");
                Writer.Write(attributes);
            }

            if (!Empty)
            {
                Writer.Write(">");
            }
        }

        protected override void OnEnd()
        {
            if (Empty)
            {
                if (Context.Options.SelfClosingEmptyTags)
                {
                    Writer.Write(" />");
                }
                else
                {
                    Writer.Write(">");
                }
            }
            else
            {
                Writer.Write($"</{TagName}>");
            }

            if (Context.IsDebug(DebugFlags.EndTag) && ContainerMode == ContainerModes.Block)
            {
                if (Attributes != null)
                {
                    if (Attributes.ContainsKey("id"))
                    {
                        Comment($"end {Attributes["id"]}");
                    }
                }
            }

        }

    }
}
