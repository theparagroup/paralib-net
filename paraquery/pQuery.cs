using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Blocks;
using com.paraquery.Core;
using com.paralib.Utils;
using com.paraquery.jQuery.Blocks;

namespace com.paraquery
{
    public class pQuery
    {
        protected Context _context { private set; get; }

        public pQuery(Context context)
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

        public Html.Attribute Attribute(string name, string value)
        {
            return new Html.Attribute(name, value);
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
            return Resources.ReadManifestResouceString(name);
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
            if (data==null)
            {
                data = new object { };
            }
            
            string template=Template("com.paraquery.jQuery.Js.ajax.js");
            string script = template.Replace("{0}", url).Replace("{1}", Json.Serialize(data, true)).Replace("{2}", targetId);
            Response.Snippet("ajax", script);
        }
    }
}
