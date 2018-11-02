using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;

namespace com.paraquery.Html
{
    public class DOCTYPE : Renderer
    {
        private string _specification;

        public DOCTYPE(Context context, string specification) : base(context, FormatModes.Line, StackModes.Line)
        {
            _specification = specification;
        }

        public DOCTYPE(Context context, DocumentTypes documentType) : this(context, DecodeDocumentType(documentType))
        {

        }

        protected static string DecodeDocumentType(DocumentTypes documentType)
        {
            switch (documentType)
            {
                case DocumentTypes.Html2:
                    return "html PUBLIC \"-//IETF//DTD HTML 2.0//EN\"";

                case DocumentTypes.Html32:
                    return "html PUBLIC \"-//W3C//DTD HTML 3.2 Final//EN\"";

                case DocumentTypes.Html401Frameset:
                    return "HTML PUBLIC \"-//W3C//DTD HTML 4.01 Frameset//EN\" \"http://www.w3.org/TR/html4/frameset.dtd\"";

                case DocumentTypes.Html401Transitional:
                    return "HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\"";

                case DocumentTypes.Html401Strict:
                    return "HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\"";

                case DocumentTypes.Html5:
                    return "HTML";

                case DocumentTypes.XHtml1Frameset:
                    return "html PUBLIC \"-//W3C//DTD XHTML 1.0 Frameset//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd\"";

                case DocumentTypes.XHtml1Transitional:
                    return "html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"";

                case DocumentTypes.XHtml1Strict:
                    return "html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"";

                case DocumentTypes.XHtmlBasic1:
                    return "html PUBLIC \"-//W3C//DTD XHTML Basic 1.0//EN\" \"http://www.w3.org/TR/xhtml-basic/xhtml-basic10.dtd\"";

                case DocumentTypes.XHtml11:
                    return "html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\"";

                default:
                    throw new Exception($"Unknown DocType {documentType}");
            }

        }

        protected override void OnBegin()
        {
            Writer.Write($"<!DOCTYPE {_specification}");
        }

        protected override void OnEnd()
        {
            Writer.Write($">");
        }
    }
}
