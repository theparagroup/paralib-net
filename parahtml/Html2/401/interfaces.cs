using com.parahtml.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml.Html401
{
    /*
        Rules

        You can go down (nest)
        You can stay at the same level when empty (img, br, etc)
        But you can't go back up

        We're inclusive, not exclusive (e.g. we ignore the fact that A cannot nest under A)?

         https://stackoverflow.com/questions/5997254/where-in-the-world-are-are-the-html-nesting-rules

    */

    public interface IFlow:IBlock, IInline
    {

    }

    public interface IBlock
    {
        IFlow Div(Action<GlobalAttributes> attributes = null);
        IFlow Div(string @class);

        IFlow Form(Action<FormAttributes> attributes = null); //exluding enclosed Form

        IBlock Hr(Action<HrAttributes> attributes); //empty

        IInline P(Action<GlobalAttributes> attributes = null);
        IInline H1(Action<GlobalAttributes> attributes = null);
        IInline H2(Action<GlobalAttributes> attributes = null);
        IInline H3(Action<GlobalAttributes> attributes = null);
        IInline H4(Action<GlobalAttributes> attributes = null);
        IInline H5(Action<GlobalAttributes> attributes = null);
        IInline H6(Action<GlobalAttributes> attributes = null);

    }

    public interface IInline
    {
        //we consider text to be inline content for simplicity
        IInline Write(string text);
        IInline WriteLine(string text);

        void Content(Action action);
        void End();

        IInline Span(Action<GlobalAttributes> attributes = null);
        IInline Span(string @class);

        IInline Br(Action<GlobalAttributes> attributes = null); //empty

        IInline Img(Action<ImgAttributes> attributes = null); //empty

        IInline A(Action<AAttributes> attributes = null); //exluding enclosed A

    }


}
