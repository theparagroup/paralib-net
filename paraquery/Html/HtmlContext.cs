using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Packages;

namespace com.paraquery.Html
{
    /*

        A version of Context that has HTML-centric functionality.


    */
    public class HtmlContext : Context
    {
        public new HtmlOptions Options { private set; get; }

        public Dictionary<Type, Package> Packages { private set; get; } = new Dictionary<Type, Package>();

        public HtmlBuilder HtmlBuilder { private set; get; }

        public HtmlContext(Writer writer, Action<HtmlOptions> options = null) : base(writer)
        {
            Options = new HtmlOptions();

            if (options != null)
            {
                options(Options);
            }

            base.Options = Options;

            HtmlBuilder = new HtmlBuilder(this);
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
