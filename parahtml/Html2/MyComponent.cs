using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Builders;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.paralib.Gen;

namespace com.parahtml.Html2
{
    public class MyComponentOptions
    {
        public string Foo;
    }

    public class MyComponent : HtmlBuilderComponent, IComponent
    {
        protected MyComponentOptions _options;

        public MyComponent(HtmlBuilder2 htmlBuilder, Action<MyComponentOptions> options):base(htmlBuilder)
        {
            _options = new MyComponentOptions();

            if (options != null)
            {
                options(_options);
            }
        }

        public void Editor(string name, string value)
        {
            Span("label");
            Write(name);
            Close();

            Span("value");
            Write(value);
            Close();

        }

    }
}
