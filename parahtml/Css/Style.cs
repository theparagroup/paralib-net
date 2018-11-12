using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;
using com.paralib.Gen;
using com.parahtml.Core;

/*
    The Style class is used in the following ways:
        
        HTML Style Attribute Declaration (inline style)
        
            <tag style="property: value; property: value;" ></tag>
                        <------------------------------>
                                  declaration    
    
        CSS Style Sheet Delaration (internal/external stylesheets)

            selector { property: value; property: value; }
                        <------------------------------>
                                  declaration    

    Together, the selector and the declaration are called a rule or ruleset:

        div 
        { 
            background-color: green; 
        }

    Declarations can be marked important:

        table td    { height: 50px !important; }

    We also have "at-rules":
        
        @import

    Most properties have two forms:

        Mixed Case (BackgroundImage)
        Camel Case (backgroundImage)

    Mixed case properties (typed) are reified with strongly typed structures.

    Camel case properties (freeform) are strings and can be used in a free-form fashion.

    We don't reify in any way:
        shorthand properties (e.g., "background: url(/foo.jpg) linear-gradient(red, green);")
        "initial"
        "inherit"

    We don't enforce any rules, we just provide strongly typed structure for creating styles.
    Of course, we could do this, by either throwing exceptions or arbitrarily choosing one
    of two elements where only one of the two is required. Rather, the goal is to help building
    CSS, but not remove the need to understand it. So if you would have made a certain mistake
    in a hand-written style, you can make the same mistake here.

    Camel-cased properties take precendent if both mixed and camel case properties have values. 
    That is, freeform properties (camel case strings) override strongly typed properties.

    Style (via StyleBase) also has a Properties object that allows you to add addtional properties 
    via an anonymous object. These properties override both freeform and typed properties.


*/

/*

    these can be nested

    border
    border-width
    border-style
    border-color
    border-bottom, etc etc

    column-rule
    colulmns

    flex
    flex-grow
    flex-shrink
    flex-basis

    font

    grid

    margin

    outline

    padding



*/


namespace com.parahtml.Css
{
    public class Style : IComplexValue<HtmlContext> //,StyleBase, IDynamicValueContainer
    {
        public string color { set; get; }
        public Color? Color { set; get; }

        public string background { get; set; }
        protected Background _background;

        public string ToValue(HtmlContext context)
        {
            var properties =context.PropertyBuilder.Properties(this);
            var style = context.PropertyBuilder.ToDeclaration(properties);
            return style;
        }


        //protected override string GetProperties()
        //{
        //    //since stylebase doesn't add a final semicolon, do it here
        //    return $"{base.GetProperties()};";
        //}

        //bool IDynamicValueContainer.HasValue(string propertyName)
        //{
        //    switch (propertyName)
        //    {
        //        case nameof(Background):
        //            return _background != null;

        //        default:
        //            throw new InvalidOperationException($"Property name {propertyName} not found");
        //    }

        //}


        //[DynamicValue]
        //public Background Background
        //{
        //    set
        //    {
        //        _background = value;
        //    }
        //    get
        //    {
        //        if (_background == null)
        //        {
        //            _background = new Background();
        //        }

        //        return _background;
        //    }
        //}


    }
}
