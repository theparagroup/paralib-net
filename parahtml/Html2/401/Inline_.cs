using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;

namespace com.parahtml.Html401
{
    public partial class FluentHtml : IInline
    {
        IInline IInline.Write(string text)
        {
            Write(text);
            return this;
        }

        IInline IInline.WriteLine(string text)
        {
            WriteLine(text);
            return this;
        }

        void IInline.Content(Action action)
        {
            if (action!=null)
            {
                action();
            }

            Finish();
        }

        void IInline.End()
        {
            Finish();
        }

        IInline IInline.Span(Action<GlobalAttributes> attributes)
        {
            Span(attributes);
            return this;
        }

        IInline IInline.Span(string @class)
        {
            Span(@class);
            return this;
        }

        IInline IInline.Br(Action<GlobalAttributes> attributes)
        {
            Br(attributes);
            return this;
        }

        IInline IInline.Img(Action<ImgAttributes> attributes)
        {
            Img(attributes);
            return this;
        }

        IInline IInline.A(Action<AAttributes> attributes)
        {
            A(attributes);
            return this;
        }

    }
}
