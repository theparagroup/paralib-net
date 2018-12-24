using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.parahtml.Html2;
using com.parahtml.Attributes;

namespace com.parahtml.Html401
{
    public partial class FluentHtml : HtmlBuilder2, IBlock
    {

        IFlow IBlock.Div(Action<GlobalAttributes> attributes)
        {
            Div(attributes);
            return this;
        }

        IFlow IBlock.Div(string @class)
        {
            Div(@class);
            return this;
        }

        IFlow IBlock.Form(Action<FormAttributes> attributes)
        {
            Form(attributes);
            return this;
        }

        IBlock IBlock.Hr(Action<HrAttributes> attributes)
        {
            Hr(attributes);
            return this;
        }

        IInline IBlock.P(Action<GlobalAttributes> attributes)
        {
            P(attributes);
            return this;
        }

        IInline IBlock.H1(Action<GlobalAttributes> attributes)
        {
            H1(attributes);
            return this;
        }

        IInline IBlock.H2(Action<GlobalAttributes> attributes)
        {
            H2(attributes);
            return this;
        }

        IInline IBlock.H3(Action<GlobalAttributes> attributes)
        {
            H3(attributes);
            return this;
        }

        IInline IBlock.H4(Action<GlobalAttributes> attributes)
        {
            H4(attributes);
            return this;
        }

        IInline IBlock.H5(Action<GlobalAttributes> attributes)
        {
            H5(attributes);
            return this;
        }

        IInline IBlock.H6(Action<GlobalAttributes> attributes)
        {
            H6(attributes);
            return this;
        }

    }

}
