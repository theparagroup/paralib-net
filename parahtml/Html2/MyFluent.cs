using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml.Html2
{
    public interface ISuperThing
    {
        IMore More();
    }

    public interface ILess
    {
        void Done();
    }

    public interface IMore
    {
        IMore More();
        ILess Less();
    }

    public class SuperThing : ISuperThing, IMore, ILess
    {
        protected HtmlBuilder2 _builder;
        protected IContent _start;

        public SuperThing(HtmlBuilder2 builder)
        {
            _builder = builder;
            _start = builder.Div("superthing");
        }

        public void Done()
        {
            _builder.Close(_start);
        }

        public IMore More()
        {
            _builder.Div("more");
            return this;
        }

        public ILess Less()
        {
            _builder.Span("less");
            return this;
        }

    }

    public static class MyFluentExtensions
    {
        public static ISuperThing Super(this HtmlBuilder2 obj)
        {
            return new SuperThing(obj);
        }
    }



}
