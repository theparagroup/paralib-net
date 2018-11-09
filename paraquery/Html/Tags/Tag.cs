using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;

namespace com.paraquery.Html.Tags
{
    /* 

        This is a utility class that converts HTML Block/Inline/Empty concepts Renderer concepts.

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
        public string TagName { private set; get; }
        public TagTypes TagType { private set; get; }
        public AttributeDictionary Attributes { private set; get; }

        internal Tag(HtmlContext context, TagTypes tagType, string tagName, AttributeDictionary attributes, bool empty = false) : base(context, GetLineMode(tagType, empty), GetContentMode(tagType, empty), empty, true, true)
        {
            TagName = tagName;
            Attributes = attributes;
        }

        public bool Empty
        {
            get
            {
                return Terminal;
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

        private static StackModes GetContentMode(TagTypes tagType, bool empty)
        {
            if (tagType == TagTypes.Block)
            {
                if (empty)
                {
                    return StackModes.Linear;
                }
                else
                {
                    return StackModes.Nested;
                }
            }
            else
            {
                return StackModes.Linear;
            }
        }

        protected override void OnDebug(string text)
        {
            var id = Attributes?["id"];

            if (id != null)
            {
                Comment($"{text} {TagName} {id}");
            }
            else
            {
                Comment($"{text} {TagName}");
            }
        }

        protected virtual void WriteAttribute(string name, string value = null)
        {
            if (name != null)
            {
                if (value == null && !Context.Options.MinimizeBooleans)
                {
                    value = name;
                }

                if (value == null)
                {
                    //boolean style attributes (e.g. "readonly")
                    Writer.Write($" {name}");
                }
                else
                {
                    if (Context.Options.EscapeAttributeValues)
                    {
                        value = value.Replace("\"", "&quot;");
                    }

                    Writer.Write($" {name}=\"{value}\"");
                }

            }
        }

        protected virtual void WriteAttributes()
        {
            if (Attributes != null)
            {
                foreach (var name in Attributes.Keys)
                {
                    WriteAttribute(name, Attributes[name]);
                }
            }
        }

        protected override void OnBegin()
        {
            Writer.Write($"<{TagName}");

            WriteAttributes();

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

            if (Context.IsDebug(DebugFlags.EndTag) && StackMode == StackModes.Nested)
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
