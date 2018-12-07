using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Html2
{
    public interface IInline
    {
        IInline Span(string @class=null);
        IInline Br();
        IInline Content(Action<IInline> action);
        IInline Write(string text);
    }

    public interface IBlock:IInline 
    {
        IBlock Div(string @class=null);
        IBlock Hr();
        IBlock Content(Action<IBlock> action);
        IBlock WriteLine(string text);
    }

    public interface IFluentHtml2 : IBlock
    {

    }

    public partial class FluentHtml2<C> : HtmlBuilderComponent<C>, IFluentHtml2 where C:HtmlContext
    {
        IBlock IBlock.Div(string @class)
        {
            Div(@class);
            return this;
        }

        IBlock IBlock.Hr()
        {
            Hr();
            return this;
        }

        IBlock IBlock.Content(Action<IBlock> action)
        {
            var marker = Open(new Marker(LineModes.Multiple, ContainerModes.Block));

            if (action!=null)
            {
                action(this);
            }

            Close(marker);

            return this;
        }

        IBlock IBlock.WriteLine(string text)
        {
            WriteLine(text);
            return this;
        }

        IInline IInline.Br()
        {
            Br();
            return this;
        }

        IInline IInline.Span(string @class)
        {
            Span(@class);
            return this;
        }

        IInline IInline.Content(Action<IInline> action)
        {
            var marker = Open(new Marker(LineModes.None, ContainerModes.Inline));

            if (action != null)
            {
                action(this);
            }

            Close(marker);

            return this;
        }

        IInline IInline.Write(string text)
        {
            Write(text);
            return this;
        }



    }
}
