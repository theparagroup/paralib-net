using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Html
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
        public AttributeDictionary Attributes { private set; get; }
        public TagTypes TagType { private set; get; }
        public bool Empty { private set; get; }

        public Tag(string tagName, AttributeDictionary attributes, TagTypes tagType, bool empty, LineModes lineMode, ContainerModes containerMode, bool indentContent) : base(lineMode, containerMode, indentContent)
        {
            TagName = tagName;
            Attributes = attributes;

            TagType = tagType;
            Empty = empty;

            if (TagName == null)
            {
                throw new InvalidOperationException("TagName cannot be null");
            }
        }

        public Tag(string tagName, AttributeDictionary attributes, TagTypes tagType, bool empty ) : this(tagName, attributes, tagType, empty, GetLineMode(tagType, empty), GetContainerMode(tagType, empty), true)
        {
        }

        public static LineModes GetLineMode(TagTypes tagType, bool empty)
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

        public static ContainerModes GetContainerMode(TagTypes tagType, bool empty)
        {
            if (empty)
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

        protected override void OnBegin()
        {
            Writer.Write($"<{TagName}");

            if (Attributes != null)
            {
                var attributes = Context.AttributeBuilder.ToTagContents(Attributes);
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

            if (LineMode== LineModes.Multiple && ContainerMode == ContainerModes.Block && Context.IsDebug(DebugFlags.EndTag))
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
