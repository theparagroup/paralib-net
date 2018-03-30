using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html;
using com.paraquery.Html.Blocks;
using com.paraquery.jQuery.Blocks;
using com.paraquery.Bootstrap.Grids;

namespace com.paraquery
{
    public class pQuery
    {
        protected IContext _context { private set; get; }
        protected Tag _tag { private set; get; }

        public pQuery(IContext context, Tag tag)
        {
            _context = context;
            _tag = tag;
        }

        /* ---------------------------------- do we want these right off of pquery? fluent? ---------------------------------------- */


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

        public void Tabs()
        {
            Response.Tabs();
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

        /* ---------------------------------- this needs to be fluent ---------------------------------------- */

        public IContext Context
        {
            get
            {
                return _context;
            }
        }

        // want this?
        public IResponse Response
        {
            get
            {
                return _context.Response;
            }
        }


        public Tag Tag
        {
            get
            {
                return _tag;
            }
        }

        public FluentGrid Grid()
        {
            return new FluentGrid(_context, _tag);
        }

        public Div Div(object attributes=null)
        {
            return new Div(_context, _tag, attributes);
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
            return new Script(_context, _tag, attributes);
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
            Response.NewLine();
        }

    }
}
