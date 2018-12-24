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

    public partial class FluentHtml:HtmlBuilder2, IFlow
    {
        protected ICloseable _start;

        public FluentHtml(HtmlBuilder2 htmlBuilder):base(htmlBuilder)
        {
        }

        public void Start()
        {
            Finish();
        }

        public override ICloseable Open(IContent content)
        {
            if (_start==null)
            {
                _start = content;
            }

            return base.Open(content);
        }

        public void Finish()
        {
            Close(_start);
            _start = null;
        }

    }

  

}
