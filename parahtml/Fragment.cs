using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.parahtml.Tags.Fluent;

namespace com.parahtml
{
    /*
        
        A base class for chunks of HTML.

        Page and other HTML components derive from this class.

    */
    public class Fragment : HtmlComponent<ParaHtmlPackage>
    {
        public Fragment(HtmlContext context, bool begin=true) : base(context, LineModes.Multiple, ContainerModes.Block, false, false)
        {
            if (begin)
            {
                Begin();
            }
        }

        protected override void OnBegin()
        {
            Push(new HtmlDebugBlock(Context, "fragment", Context.IsDebug(DebugFlags.Fragment), false));
        }

        protected override void OnEnd()
        {
        }

        public FluentHtml Html(bool inline = false)
        {
            var fluentHtml = new FluentHtml(Context, inline ? LineModes.None : LineModes.Multiple, inline ? ContainerModes.Inline : ContainerModes.Block, false);
            Push(fluentHtml);
            return fluentHtml;
        }

        public FluentHtml Html(Action<FluentHtml> fluentHtml, bool inline = false)
        {
            var fh = Html(inline);

            if (fluentHtml != null)
            {
                fluentHtml(fh);
            }

            return fh;
        }


    }
}
