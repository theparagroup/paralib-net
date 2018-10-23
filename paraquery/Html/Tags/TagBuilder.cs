using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;

namespace com.paraquery.Html.Tags
{

    public partial class TagBuilder
    {
        protected IContext _context;

        public TagBuilder(IContext context)
        {
            _context = context;
        }

        public IContext Context
        {
            get
            {
                return _context;
            }
        }

        protected virtual void Write(string text)
        {
            _context.Writer.Write(text);
        }

        public virtual void Attribute(string name, string value=null)
        {
            //TODO escaping quotes? escaping in general?

            if (name != null)
            {
                if (value==null)
                {
                    //boolean
                    Write($" {name}");
                }
                else
                {
                    Write($" {name}=\"{value}\"");
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

            var atts = AttributeDictionary.Build(attributes);

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

        public object Attributes<T>(Action<T> init = null, object additional = null) where T : GlobalAttributes, new()
        {

            if (init != null)
            {
                T attributes = new T();
                init(attributes);

                //TODO process known nested objects such as style


                if (additional != null)
                {
                    //note we double nest here to ensure the "additional" are secondary to the "attributes"
                    return new { attributes = attributes, additional = new { additional = additional } };
                }
                else
                {
                    return attributes;
                }

            }
            else
            {
                return additional;
            }

        }

        public string GetAttribute(object attributes, string name)
        {
            //TODO make this work

            if (attributes is GlobalAttributes)
            {
                return ((GlobalAttributes)attributes).Id ?? "";
            }

            return "";
        }


        public virtual void Start(string name, object attributes = null)
        {
            Write($"<{name}");
            Attributes(attributes);
        }

        public virtual void End()
        {
            Write(">");
        }

        public virtual void Open(string name, object attributes = null)
        {
            Start(name, attributes);
            End();
        }

        public virtual void Close(string name)
        {
            Write($"</{name}>");
        }

        public virtual void Empty(string name, object attributes = null)
        {
            Write($"<{name}");
            Attributes(attributes);
            Write(" />");
        }


       


    }
}
