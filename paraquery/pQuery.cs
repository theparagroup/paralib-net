using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html;
using com.paraquery.jQuery.Blocks;
using com.paraquery.Bootstrap;
using com.paraquery.Js.Blocks;
using com.paraquery.Html.Fluent;
using com.paraquery.Html.Tags;

namespace com.paraquery
{
    public class pQuery
    {
        protected HtmlBuilder _htmlBuilder;
        protected HtmlContext _context;
        protected Bs _bs { set; get; }

        public pQuery(HtmlContext context)
        {
            _context = context;
            _htmlBuilder = new HtmlBuilder(context);

            _bs = new Bs(context);

        }

        public HtmlContext Context
        {
            get
            {
                return _context;
            }
        }

        public Writer Writer
        {
            get
            {
                return _context.Writer;
            }
        }

        public HtmlBuilder HtmlBuilder
        {
            get
            {
                return _htmlBuilder;
            }
        }

        public FluentHtml Html()
        {
            return new FluentHtml(_context);
        }

        public Bs Bs
        {
            get
            {
                return _bs;
            }
        }


        //fold into Js
        public Function Function(string name, params string[] parameters)
        {
            return new Function(_context, name, parameters);
        }

        public void Alert(string message)
        {
            Writer.WriteLine($"alert('{message}');");
        }

        //fold into jquery/js
        public Click Click(string selector, object data=null)
        {
            return new Click(_context, selector, data);
        }

        public void Click(string selector, string js, object data = null)
        {
            Writer.WriteLine($"$('{selector}').click({Utils.Parameters(data)}function(event){{ {js} }} );");
        }

        public Ready Ready()
        {
            return new Ready(_context);
        }

        public void Ajax(string url, string targetId, object data=null)
        {
            //prefix - TODO refactor this out
            url = _context.UrlPrefix(url??"");

            //default data
            string dataString = "data";
            if (data!=null)
            {
                dataString = Utils.Json.Serialize(data, true);
            }
            
            string template=Utils.Template("com.paraquery.jQuery.Templates.ajax.js");
            string script = template.Replace("{0}", url).Replace("{1}", dataString).Replace("{2}", targetId);
            Writer.Snippet(script);
        }

    }
}
