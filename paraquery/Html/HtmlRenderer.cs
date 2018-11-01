using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;
using com.paraquery.Html.Tags;

namespace com.paraquery.Html
{
    public abstract class HtmlRenderer : Renderer
    {
        protected TagBuilder TagBuilder { private set; get; }

        public HtmlRenderer(Context context, FormatModes formatMode, StackModes stackMode) : base(context, formatMode, stackMode)
        {
            TagBuilder = new TagBuilder(context);
        }

        public static void Comment(Writer writer, string text)
        {
            writer.Write($"<!-- {text} -->");
        }

        public void Comment(string text)
        {
            Writer.Write($"<!-- {text} -->");
        }

        protected override void OnDebug(string text)
        {
            Comment(text);
        }


    }
}
