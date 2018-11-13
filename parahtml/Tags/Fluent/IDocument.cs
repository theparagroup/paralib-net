using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;
using com.paralib.Gen.Fluent;

namespace com.parahtml.Tags.Fluent
{
    public interface IDocument : IFluentStack<Document>
    {
        IDocument DOCTYPE(DocumentTypes documentType);
        IDocument DOCTYPE(string specification);
        IDocument Html(Action<HtmlAttributes> attributes = null);
        IDocument Head(Action<HeadAttributes> attributes = null);
        IDocument Title(Action<GlobalAttributes> attributes = null);
        IDocument Style(Action<StyleAttributes> attributes = null);
        IDocument Script(Action<ScriptAttributes> attributes = null);
        IDocument Body(Action<BodyAttributes> attributes = null);

    }
}
