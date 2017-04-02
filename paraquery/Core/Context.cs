using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Core
{
    public class Context
    {
        public IServer Server { private set; get; }
        public IRequest Request { private set; get; }
        public IResponse Response { private set; get; }
        //namespace stack
        //namespace vars

        public Context(IServer server, IRequest request, IResponse response, string @namespace, Dictionary<string,string> namespaceVars)
        {
            Server = server;
            Request = request;
            Response = response;

            //add initial namespace

        }

        

        public void Attributes(object attributes=null)
        {
            //TODO allow for name substitutions (clazz->class, classes->class)
            //TODO allow for symbol replacement in names ( data_value -> data-value, data__value -> data_value)
            //TODO allow for variables? (style="debug")
            //TODO allow for expansions? new { id="foo", style=new { background_color="green"}, margin=new { border_style="solid" } }

            // id="[foo]" -> id="ns-foo"
            // id="[admin:foo]" -> id="nsvar-foo"
            // id="foo" -> id="foo"

            var atts = Html.Attribute.ToDictionary(attributes);

            if (atts != null)
            {
                if (atts.ContainsKey("id"))
                {
                    //TODO process namespaces for ids
                    Response.Attribute("id", atts["id"]);
                }

                if (atts.ContainsKey("class"))
                {
                    //TODO process namespaces for classes
                    Response.Attribute("class", atts["class"]);
                }

                foreach (var key in atts.Keys)
                {
                    if (key!="id" && key!="class")
                    {
                        Response.Attribute(key, atts[key]);
                    }
                }

            }
        }


    }
}
