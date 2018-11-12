﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace com.paraquery.Html.Tags
{
    /*

        DictionaryBuilder, builds html attribute and css property dictionaries from strongly typed
        objects. 


    */
    public class DictionaryBuilder
    {
        protected Context Context {private set; get;}

        public DictionaryBuilder(Context context)
        {
            Context = context;
        }

        public void Build(NVPDictionary dictionary, object attributes, bool caseSensive)
        {
            Build(dictionary, attributes, attributes.GetType(), caseSensive);
        }

        public void Build(NVPDictionary dictionary, object attributes, Type type, bool caseSensive)
        {
            if (attributes != null)
            {
                //this includes inherited properties (see missing DeclaredOnly flag)
                //this also excludes interface properties implemented explictly
                BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty;

                //enumerate properties
                var pis = type.GetProperties(bindingFlags);

                foreach (var pi in pis)
                {
                    //we don't bother unless there is a getter, even for IComplexValue types
                    if (pi.CanRead)
                    { 
                        //we assume that the property does indeed have a value
                        //but the presence of DynamicValueAttribute allows
                        //us to check the backing store without invoking the
                        //getter
                        bool hasValue = true;

                        //handle dynamic values
                        if (pi.GetCustomAttribute<DynamicValueAttribute>() != null)
                        {
                            if (typeof(IDynamicValueContainer).IsAssignableFrom(type))
                            {
                                hasValue = ((IDynamicValueContainer)attributes).HasValue(pi.Name);
                            }
                        }

                        //v is the "raw" value - we'll process it below
                        object v = null;

                        //if we still have a value to read, let's read it
                        //otherwize v stays null
                        if (hasValue)
                        {
                            //unless of course the actual property
                            v = pi.GetValue(attributes);
                        }

                        if (v != null)
                        {

                            //if you have two properties with same name but different case, we will enumerate both
                            //but the order is undefined. the last one will replace the first one in the dictionary
                            //if we are in case-insensitive mode. otherwise both will be added.


                            //let's do the name
                            string name = null;

                            //see if the property has explicit builder options
                            var options = pi.GetCustomAttribute<BuilderOptionsAttribute>();
                            if (options?.Name != null)
                            {
                                name = options.Name;
                            }
                            else if (typeof(IValueContainer).IsAssignableFrom(pi.PropertyType))
                            {
                                //if this is a "value container" (meaning it doesn't render as a name/value
                                //pair but as just the value), we prefix the name with a bang, which is not
                                //a valid HTML attribute name. this is mainly used by Style to organize
                                //properties into nested classes (see Background). So if you are using this
                                //interface, you need to do something with these bangs.
                                name = $"{Context.Options.ValueContainerMarker}{pi.Name}";
                            }
                            else
                            {
                                //generally, we're always in case-insensitive mode, and by default attribute
                                //names are lower case. however, in certain cases such as Style, it is useful
                                //to build a dictionary of name/values with mixed case
                                if (caseSensive)
                                {
                                    name = pi.Name;
                                }
                                else
                                {
                                    name = pi.Name.ToLower();
                                }
                            }

                            //let's do the value. v is the raw, value is the processed.
                            string value = null; 

                            if (options?.Value!=null)
                            {
                                //if [Attribute] was present and a value provided, use that
                                value = options?.Value;
                            }
                            else if (typeof(IComplexValue).IsAssignableFrom(pi.PropertyType))
                            {
                                //if complex, use that
                                value = ((IComplexValue)v).ToValue(Context);
                            }
                            else if (pi.PropertyType == typeof(string))
                            {
                                value = (string)v;
                            }
                            else if (pi.PropertyType == typeof(int) || pi.PropertyType == typeof(int?))
                            {
                                value = v.ToString();
                            }
                            else if (pi.PropertyType == typeof(float) || pi.PropertyType == typeof(float?))
                            {
                                value = v.ToString();
                            }
                            else if (pi.PropertyType == typeof(bool) || pi.PropertyType == typeof(bool?))
                            {
                                //this cast is okay since we know v isn't null (if nullable)
                                value = v.ToString().ToLower();
                            }
                            else if ((pi.PropertyType.IsEnum) || (Nullable.GetUnderlyingType(pi.PropertyType)?.IsEnum ?? false))
                            {
                                value = v.ToString().ToLower();
                            }
                            else
                            {
                                //process unknown types recursively, looking for anything we can add to the dicitonary
                                Build(dictionary, v, caseSensive);
                                continue;
                            }

                            //again, at this point, "v" isn't null, but "value" may be...
                            //add it to the dictionary
                            OnAdd(Context, dictionary, name, value); 

                        }//if v!=null
                    } //if canread
                }//foreach pi
            }

        }

        protected virtual void OnAdd(Context context, NVPDictionary dictionary, string name, string value)
        {
            dictionary.Add(name, value);
        }

    }
}


/*

   case sensitivity

   CSS is case insensitive

   however, the following things are case sensitive:

       ids
       class names
       urls
       font names/families?


   in html, tag names are case insensitive

   tag names in html5?

   in xhtml, tag names are case sensitive




   anything else?


*/

//examples
// "class1 class2"
// new { id="div1", @class="class1 class2"}
// new { id="div1", defaults= new { @class="class1 class2"}}

/*

    the idea here is to accept
      a string, which is assumed to a list of classes (shorthand)
      a dictionary of name/value pairs
      an (anonymous) object
          containing either string properties
          or a nested (anonymous) object

      the string, int, bool, enum, etc properties will be used for name value pairs
      properties on additional objects will be used as "defaults" (not added if they already exist)
          unless the property is "class" then it will be merged

      properties can be IComplexAttribute

      recursion and complex handling can be toggled

      property names are lower cased by default but can be converted from camel case to hypenated


*/

/*
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
*/
