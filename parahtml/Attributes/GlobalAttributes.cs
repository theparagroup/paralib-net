using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;
using com.parahtml.Css;
using com.parahtml.Core;
using com.paralib.Gen;

namespace com.parahtml.Attributes
{
    /*

        Base class for all html attributes.
        
        
        In HTML5, Global Attributes can be used on *any* element.

        In other versions, this isn't true. For example, in HTML 4.01, you can't 
        put a style attribute on the following:

            <base>
            <head>
            <html>
            <meta>
            <param>
            <script>
            <style>
            <title>

        We don't attempt to enforce these kinds of rules.


        More stuff to do:

            accesskey
            dir
            hidden


            new in HTML5
                contenteditable
                data-*
                draggable
                dropzone
                spellcheck
                translate
*/

    public class GlobalAttributes:StyleBase, IHasContext<HtmlContext>
    {
        protected HtmlContext Context { private set; get; }

        public object Attributes { get; set; }

        void IHasContext<HtmlContext>.SetContext(HtmlContext context)
        {
            Context = context;
        }

        public string Id { set; get; }
        public string id { set; get; }

        public string Class { set; get; }
        public string @class { set; get; }

        public int? TabIndex { get; set; }
        public string tabindex { get; set; }

        public string title { get; set; }

        public string lang { get; set; }

        [DynamicValue]
        public Style Style => _get<Style>(nameof(Style));
        public string style { get; set; }



    }
}
