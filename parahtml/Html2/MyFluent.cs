using com.parahtml.Core;
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
        public void Done()
        {
            
        }

        public ILess Less()
        {
            return this;
        }

        public IMore More()
        {
            return this;
        }
    }

    public static class MyFluentExtensions
    {
        public static ISuperThing Super(this HtmlContext obj)
        {
            return null;
        }

    }



}
