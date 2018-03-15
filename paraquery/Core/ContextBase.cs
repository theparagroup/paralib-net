using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Core
{
    public abstract class ContextBase:IContext
    {
        public IServer Server { private set; get; }
        public IRequest Request { private set; get; }
        public IResponse Response { private set; get; }
        //namespace stack
        //namespace vars

        public ContextBase(IServer server, IRequest request, IResponse response, string @namespace, Dictionary<string,string> namespaceVars)
        {
            Server = server;
            Request = request;
            Response = response;

            //add initial namespace

        }

        

        public void Attributes(object attributes=null)
        {
            //TODO allow for namespaces
            //TODO allow for namespace-vars (class="[admin:foo-bar]" -> class="nsvar-foo-bar") 
            //TODO allow for name substitutions (clazz->class, classes->class)
            //TODO allow for symbol replacement (_,-) in names ( data_value -> data-value, data__value -> data_value)
            //TODO allow for variables? (class="{debug}" -> class="debug-verbose")
            //TODO allow for expansions? new { id="foo", style=new {background_color="green"}, margin=new { border_style="solid" } } -> id="foo" style="backgound-color:green;" margin="border-style:solid;"

            // id="foo-bar" -> id="foo-bar" (no change)
            // id="[foo-bar]" -> id="ns-foo-bar" (ns prefixing)
            // id="[blah:admin:foo-bar]" -> id="blah-admin-foo-bar" (if admin not an nsvar)
            // id="[:blah:admin:foo-bar]" -> id="ns-blah-admin-foo-bar" (with current ns)
            // id="[blah:admin:foo-bar]" -> id="blah-nsvar-foo-bar" (if admin is an nsvar)

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
