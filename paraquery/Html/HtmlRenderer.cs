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
        public HtmlRenderer(Context context, FormatModes formatMode, StructureModes structureMode, bool empty = false, bool indent = true) : base(context, formatMode, structureMode, empty, indent)
        {
        }

        public static void Comment(Writer writer, string text)
        {
            writer.Write($"<!-- {text} -->");
        }

        public void Comment(string text)
        {
            Comment(Writer, text);
        }

        protected override void OnDebug(string text)
        {
            Comment(text);
        }

    }
}
