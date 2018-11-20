using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Mapping;

namespace com.parahtml
{
    public enum RadialGradientShapes
    {
        Circle,
        Ellipse
    }

    public enum RadialGradientSizes
    {
        ClosestSide,
        FarthestSide,
        ClosestCorner,
        FarthestCorner
    }

    public enum RadialGradientPositions
    {
        Left,
        Right,

        Center,

        Top,
        Bottom,

        LeftTop,
        LeftCenter,
        LeftBottom,

        RightTop,
        RightCenter,
        RightBottom,

        CenterCenter

    }

    public enum RadialGradientVPositions
    {
        Top,
        Center,
        Bottom
    }

    public enum RadialGradientVPositions2
    {
        Top,
        Bottom
    }

    public enum RadialGradientHPositions
    {
        Left,
        Center,
        Right
    }

    public enum RadialGradientHPositions2
    {
        Left,
        Right
    }

    public class RadialGradientOptions:ComplexValue<HtmlContext>
    {
        protected string _shape;
        protected string _size;
        protected string _position;

        protected override string Value
        {
            get
            {
                string value = null;

                if (_shape!=null)
                {
                    value = _shape;
                }

                if (_size!=null)
                {
                    if (value != null)
                    {
                        value += " ";
                    }

                    value += _size;
                }

                if (_position != null)
                {
                    if (value != null)
                    {
                        value += " ";
                    }

                    value += $"at {_position}";
                }

                return value;
            }
        }

        public RadialGradientShapes Shape
        {
            set
            {
                _shape = value.ToString().ToLower();

            }
        }

        public void Size(RadialGradientSizes size)
        {
            _size = HtmlBuilder.HyphenateMixedCase(size.ToString()).ToLower();
        }

        public void Size(Length length)
        {
            _size = length.ToString();
        }

        public void Size(LengthOrPercentage x, LengthOrPercentage y)
        {
            _size = $"{x.ToString()} {y.ToString()}";
        }

        public void Position(RadialGradientPositions position)
        {
            _position = HtmlBuilder.SpacenateMixedCase(position.ToString()).ToLower();
        }

        public void Position(LengthOrPercentage horizontal, RadialGradientVPositions vertical)
        {
            _position = $"{horizontal} {HtmlBuilder.SpacenateMixedCase(vertical.ToString()).ToLower()}";
        }

        public void Position(RadialGradientHPositions horizontal, LengthOrPercentage vertical)
        {
            _position = $"{HtmlBuilder.SpacenateMixedCase(horizontal.ToString()).ToLower()} {vertical}";
        }

        public void Position(LengthOrPercentage horizontal, LengthOrPercentage vertical)
        {
            _position = $"{horizontal} {vertical}";
        }

        public void Position(RadialGradientHPositions2 horizontal, LengthOrPercentage h, RadialGradientVPositions2 vertical, LengthOrPercentage v)
        {
            _position = $"{horizontal} {h} {vertical} {v}";
        }


    }
}
