using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen;
using com.parahtml.Packages;

namespace com.parahtml.Core
{
    /*

        A version of Context that has HTML-centric functionality.


    */
    public class HtmlContext : Context
    {
        public new HtmlOptions Options { private set; get; }
        public Dictionary<Type, Package> Packages { private set; get; } = new Dictionary<Type, Package>();
        public AttributeBuilder AttributeBuilder { private set; get; }
        public PropertyBuilder PropertyBuilder { private set; get; }
        public HtmlBuilder HtmlBuilder { private set; get; }

        protected HtmlContext(Writer writer):base(writer)
        {
            AttributeBuilder = new AttributeBuilder(this);
            PropertyBuilder = new PropertyBuilder(this);
            HtmlBuilder = new HtmlBuilder(this);
        }

        public HtmlContext(Writer writer, HtmlOptions options = null) : this(writer)
        {
            Options = options ?? new HtmlOptions();
        }

        public HtmlContext(Writer writer, Action<HtmlOptions> options = null) : this(writer)
        {
            Options = new HtmlOptions();

            if (options != null)
            {
                options(Options);
            }

            base.Options = Options;

        }

        public override void Comment(string text)
        {
            Writer.Write($"<!-- {text} -->");
        }

        public bool IsDebug(DebugFlags debugFlags)
        {
            return (Options.Debug & debugFlags) != 0;
        }

        public Package RegisterPackage<T>() where T : Package, new()
        {
            if (Packages.ContainsKey(typeof(T)))
            {
                return Packages[typeof(T)];
            }
            else
            {
                T package = new T();

                //check that dependencies have been added, scripts, styles, throw if not



                Packages.Add(typeof(T), package);
                return package;
            }

        }

    }
}
