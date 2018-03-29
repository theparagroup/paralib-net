using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Blocks;
using com.paraquery.Core;
using com.paraquery.jQuery.Blocks;
using com.paraquery.Bootstrap.Grids;

namespace com.paraquery
{
    public class pQuery
    {
        protected IContext _context { private set; get; }

        public pQuery(IContext context)
        {
            _context = context;
        }

        public pQuery(string urlPrefix = null, string @namespace=null, Dictionary<string, string> namespaceVars=null)
        {
            _context = new StringContext.Context(urlPrefix,@namespace,namespaceVars);
        }

        public IResponse Response
        {
            get
            {
                return _context.Response;
            }
        }

        public void Write(string text)
        {
            Response.Write(text);
        }

        public void WriteLine(string text)
        {
            Response.WriteLine(text);
        }

        public void Tab()
        {
            Response.Tab();
        }

        public void Indent()
        {
            Response.Indent();
        }

        public void Dedent()
        {
            Response.Dedent();
        }

        public void NewLine()
        {
            Response.NewLine();
        }

        //public Html.Attribute Attribute(string name, string value)
        //{
        //    return new Html.Attribute(name, value);
        //}


        public FluentGrid Grid()
        {
            return new FluentGrid(_context);
        }


        public Div Div(object attributes=null)
        {
            return new Div(_context, attributes);
        }

        public Function Function(string name, params string[] parameters)
        {
            return new Function(_context, name, parameters);
        }

        public Click Click(string selector, object data=null)
        {
            return new Click(_context, selector, data);
        }

        public void Click(string selector, string js, object data = null)
        {
            WriteLine($"$('{selector}').click({Utils.Parameters(data)}function(event){{ {js} }} );");
        }

        public Script Script(object attributes = null)
        {
            return new Script(_context, attributes);
        }

        public Ready Ready()
        {
            return new Ready(_context);
        }

        public string Template(string name)
        {
            //TODO razor option
            //TODO error checking and caching
            return Utils.Resources.ReadManifestResouceString(System.Reflection.Assembly.GetCallingAssembly(), name);
        }

        public void Alert(string message)
        {
            Response.WriteLine($"alert('{message}');");
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
            
            string template=Template("com.paraquery.jQuery.Js.ajax.js");
            string script = template.Replace("{0}", url).Replace("{1}", dataString).Replace("{2}", targetId);
            Response.Snippet("ajax", script);
        }

    }
}
