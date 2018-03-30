using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html
{
    public class Tag
    {
        protected IContext _context;

        public Tag(IContext context)
        {
            _context = context;
        }

        protected virtual void Write(string text, bool indent=false)
        {
            _context.Response.Write(text, indent);
        }

        public virtual void Attribute(string name, string value=null)
        {
            //TODO escaping quotes? escaping in general?

            if (name != null)
            {
                if (value==null)
                {
                    //boolean
                    Write($" {name}", false);
                }
                else
                {
                    Write($" {name}=\"{value}\"", false);
                }

            }
        }

        public void Attributes(object attributes = null)
        {
            //namespace state should be over in context

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

            var atts = Utils.ToDictionary(attributes);

            if (atts != null)
            {
                if (atts.ContainsKey("id"))
                {
                    //TODO process namespaces for ids
                    Attribute("id", atts["id"]);
                }

                if (atts.ContainsKey("class"))
                {
                    //TODO process namespaces for classes
                    Attribute("class", atts["class"]);
                }

                foreach (var key in atts.Keys)
                {
                    if (key != "id" && key != "class")
                    {
                        Attribute(key, atts[key]);
                    }
                }

            }
        }

        public virtual void Start(string name, object attributes = null, bool indent=false)
        {
            Write($"<{name}", indent);
            Attributes(attributes);
        }

        public virtual void End()
        {
            Write(">", false);
        }

        public virtual void Open(string name, object attributes = null, bool indent = false)
        {
            Start(name, attributes, indent);
            End();
        }

        public virtual void Close(string name, bool indent = false)
        {
            Write($"</{name}>", indent);
        }

        public virtual void Empty(string name, object attributes = null, bool indent = false)
        {
            Write($"<{name}", indent);
            Attributes(attributes);
            Write(" />");
        }


    }
}
