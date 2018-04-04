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

namespace com.paraquery
{
    public class pQuery
    {
        protected IContext _context { set; get; }
        protected TagBuilder _tagBuilder { set; get; }
        protected Bs _bs { set; get; }

        public pQuery(IContext context, TagBuilder tagBuilder)
        {
            _context = context;
            _tagBuilder = tagBuilder;

            _bs = new Bs(_context, _tagBuilder);

        }

        public IContext Context
        {
            get
            {
                return _context;
            }
        }

        public IResponse Response
        {
            get
            {
                return _context.Response;
            }
        }

        public IRequest Request
        {
            get
            {
                return _context.Request;
            }
        }

        public IServer Server
        {
            get
            {
                return _context.Server;
            }
        }

        public TagBuilder TagBuilder
        {
            get
            {
                return _tagBuilder;
            }
        }

        public FluentHtml Html()
        {
            return new FluentHtml(_context, _tagBuilder);
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
            Response.WriteLine($"alert('{message}');");
        }

        //fold into jquery/js
        public Click Click(string selector, object data=null)
        {
            return new Click(_context, selector, data);
        }

        public void Click(string selector, string js, object data = null)
        {
            Response.WriteLine($"$('{selector}').click({Utils.Parameters(data)}function(event){{ {js} }} );");
        }

        public Ready Ready()
        {
            return new Ready(_context);
        }

        public void Ajax(string url, string targetId, object data=null)
        {
            //prefix
            url = _context.Server.UrlPrefix(url??"");

            //default data
            string dataString = "data";
            if (data!=null)
            {
                dataString = Utils.Json.Serialize(data, true);
            }
            
            string template=Utils.Template("com.paraquery.jQuery.Templates.ajax.js");
            string script = template.Replace("{0}", url).Replace("{1}", dataString).Replace("{2}", targetId);
            Response.Snippet("ajax", script);
        }

    }
}
