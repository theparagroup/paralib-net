using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;
using com.parahtml.Css;

namespace com.parahtml.Attributes
{
    /*
        In HTML5, Global Attributes can be used on *any* element.

        In other versions, this isn't true. For example, in HTML 4.01, you can't 
        put a style attrbiute on the following:

            <base>
            <head>
            <html>
            <meta>
            <param>
            <script>
            <style>
            <title>

        We don't attempt to enforce these kinds of rules.

    */

    public class GlobalAttributes:IDynamicValueContainer
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public int? TabIndex { get; set; }
        public string Title { get; set; }

        /*
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

        public string Lang { get; set; }

        public Action<Style> StyleAction
        {
            set
            {
                if (value!=null)
                {
                    var style = new Style();

                    value(style);

                    _style = style;

                }

            }
        }

        protected Style _style;

        [DynamicValue]
        public Style Style
        {
            set
            {
                _style = value;
            }
            get
            {
                if (_style==null)
                {
                    _style = new Style();
                }

                return _style;
            }
        }

        bool IDynamicValueContainer.HasValue(string propertyName)
        {
            //we only have dynamic value so far
            return (_style != null);
        }


        public object Attributes { get; set; }

    }
}
