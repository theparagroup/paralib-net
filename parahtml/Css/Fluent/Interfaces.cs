using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Tags.Fluent;

namespace com.parahtml.Css.Fluent
{
    public interface ICss<F> where F:class
    {
        //at-rules
        ICss<F> Rule(Action<ISelectorLevel> selector);
        ICss<F> Rule(string selector);
        ICss<F> Declaration(Action<Style> declaration);
        ICss<F> Declaration(string declaration);
        F Html();
        void Close();
    }


    public interface ISelectorLevel : IAttributeLevel
    {
        //types
        IAttributeLevel All(); //*
        IAttributeLevel Div();
        IAttributeLevel Span();
        IAttributeLevel P();
        //...



    }

    public interface IAttributeLevel
    {
        IAttributeSelector Attribute(string name, bool caseSensitive = true);
        IAttributeLevel Class(string @class);
        IAttributeLevel Id(string id);
        IAttributeLevel PseudoClass(string @class);
        IAttributeLevel Selector(string name);

        ISelectorLevel PseudoElement(string element);
        ISelectorLevel IsDescendentOf();
        ISelectorLevel IsChildOf();
        ISelectorLevel Follows();


    }

    public interface IAttributeSelector
    {
        IAttributeLevel Exists();
        IAttributeLevel Matches(string value);
        IAttributeLevel MatchesBeforeHyphen(string value);
        IAttributeLevel StartsWith(string value);
        IAttributeLevel EndsWith(string value);
        IAttributeLevel Contains(string value);
    }

}
