using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;
using com.parahtml.Css.Fluent;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Tags.Fluent
{
    public partial class Html
    {

        public virtual Html Open<T>(T renderer, Action<T> action) where T : Renderer
        {
            Push(renderer);
            action(renderer);
            return this;
        }

        public Html Write(string content)
        {
            Context.Writer.Write(content);
            return this;
        }

        private void CloseUpIfTopNotMultipleLine()
        {
            if (Top?.LineMode != LineModes.Multiple)
            {
                CloseUp();
            }
        }

        public Html WriteLine(string content)
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.WriteLine(content);
            return this;
        }

        public Html NewLine()
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.NewLine();
            return this;
        }

        public Html Space()
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.Space();
            return this;
        }

        public Html Snippet(string text, string newline = null)
        {
            CloseUpIfTopNotMultipleLine();
            Context.Writer.Snippet(text, newline);
            return this;
        }

        public virtual Html Tag(TagTypes tagType, string name, Action<GlobalAttributes> attributes = null, bool empty=false)
        {
            if (tagType==TagTypes.Block)
            {
                return Push(HtmlBuilder.Block(name, attributes, empty));
            }
            else
            {
                return Push(HtmlBuilder.Inline(name, attributes, empty));
            }
        }

        public virtual Html DoIt(Action<Html> action)
        {
            if (action != null)
            {
                action(this);
            }

            return this;
        }

        public virtual Html Div(Action<GlobalAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Div(attributes));
        }

        public virtual Html Span(Action<GlobalAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Span(attributes));
        }

        public virtual Html Br(Action<GlobalAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Br(attributes));
        }

        public virtual Html Hr(Action<HrAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Hr(attributes));
        }

        public virtual Html Script(Action<ScriptAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Script(attributes));
        }

        public virtual Html NoScript(Action<GlobalAttributes> attributes = null)
        {
            return Push(HtmlBuilder.NoScript(attributes));
        }

    }
}
