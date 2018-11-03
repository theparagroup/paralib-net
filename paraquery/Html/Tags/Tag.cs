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
        protected string _tagName;
        protected bool _empty;
        protected AttributeDictionary _attributes;

        public Tag(Context context, string tagName, bool block, bool empty, AttributeDictionary attributes):base(context, GetFormatMode(block, empty), GetStackMode(block, empty))
        {
            _tagName = tagName;
            _empty = empty;
            _attributes = attributes;
        }

        private static FormatModes GetFormatMode(bool block, bool empty)
        {
            if (block)
            {
                if (empty)
                {
                    return FormatModes.Line;
                }
                else
                {
                    return FormatModes.Block;
                }
            }
            else
            {
                return FormatModes.None;
            }

        }

        private static StackModes GetStackMode(bool block, bool empty)
        {
            if (block)
            {
                if (empty)
                {
                    return StackModes.Line;
                }
                else
                {
                    return StackModes.Block;
                }
            }
            else
            {
                return StackModes.Inline;
            }

        }

        protected override void OnDebug(string text)
        {
            Comment($"{text} {_tagName} {_attributes?["id"]}");
        }

        protected virtual void Attribute(string name, string value = null)
        {
            //TODO escaping quotes? escaping in general?

            if (name != null)
            {
                if (value == null)
                {
                    //boolean style attributes (e.g. "readonly")
                    Writer.Write($" {name}");
                }
                else
                {
                    Writer.Write($" {name}=\"{value}\"");
                }

            }
        }

        protected virtual void Attributes()
        {
            if (_attributes != null)
            {
                foreach (var name in _attributes.Keys)
                {
                    Attribute(name, _attributes[name]);
                }
            }
        }

        protected override void OnBegin()
        {
            Writer.Write($"<{_tagName}");

            Attributes();

            if (!_empty)
            {
                Writer.Write(">");
            }
        }

        protected override void OnEnd()
        {
            if (_empty)
            {
                if (Context.Options.SelfClosingTags)
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
                Writer.Write($"</{_tagName}>");
            }

            if (Context.Options.CommentEndTags && StackMode==StackModes.Block)
            {
                if (_attributes?.ContainsKey("id") ?? false)
                {
                    Comment($"end {_attributes?["id"]}");
                }
            }

        }

    }
}
