using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;
using com.parahtml.Core;

namespace com.parahtml
{
    /*

        Length is percentage.

        length can be <length> or <percentage>, <length> having units such as em, px, etc.


    */
    public class ColorStop : ComplexValue<HtmlContext>
    {
        protected string _colorStop { get; }
        protected Color? _color { get; }
        protected string _lengthOrPercentage { get; }

        public ColorStop(string colorStop)
        {
            _colorStop = colorStop;
        }

        public ColorStop(Color color)
        {
            _color = color;
        }

        public ColorStop(Color color, Length length) : this(color)
        {
            _lengthOrPercentage = length.ToString();
        }

        public ColorStop(Color color, Percentage percentage) : this(color)
        {
            _lengthOrPercentage = percentage.ToString();
        }

        protected override string Value
        {
            get
            {
                if (_colorStop != null)
                {
                    return _colorStop;
                }
                else
                {
                    if (_lengthOrPercentage == null)
                    {
                        return _color.ToString().ToLower();
                    }
                    else
                    {
                        return $"{_color.ToString().ToLower()} {_lengthOrPercentage}";
                    }
                }
            }
        }
    }
}
