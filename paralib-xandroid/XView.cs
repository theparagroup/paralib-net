using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using com.paralib.Xandroid.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid
{
    public class XView
    {
        public static int GetTextAppearance(XSizes size)
        {
            switch (size)
            {
                case XSizes.Small:
                    return Android.Resource.Style.TextAppearanceSmall;
                case XSizes.Medium:
                    return Android.Resource.Style.TextAppearanceMedium;
                case XSizes.Large:
                    return Android.Resource.Style.TextAppearanceLarge;
                default:
                    throw new ParalibException("bad size");
            }


        }

        public static TextView TextView(Context context, XSizes size, string text=null, Color? color = null, int? id = null, string tag = null)
        {
            var view = new TextView(context);
            view.Text = text;

            if (id.HasValue) view.Id = id.Value;
            if (tag != null) view.Tag = tag;

            //order matters (do this before other appearance changes)
            view.SetTextAppearance(context, GetTextAppearance(size));
            view.SetTextColor(color ?? Color.Black);

            return view;
        }

        public static TextView TextView(Context context, ViewGroup.LayoutParams layoutParams, XSizes size = XSizes.Medium, string text = null, Color? color = null, GravityFlags? gravity = null, int? id = null, string tag=null)
        {
            var view = TextView(context, size, text,color,id,tag);

            view.LayoutParameters = layoutParams;

            if (gravity.HasValue) view.Gravity = gravity.Value;

            return view;
        }



        public static EditText EditText(Context context, ViewGroup.LayoutParams layoutParams, XSizes size = XSizes.Medium, string text = null, Color? color = null, bool password=false, XImeActions? imeAction=null, GravityFlags? gravity = null, int? id = null, string tag = null)
        {
            var view = new EditText(context) { LayoutParameters = layoutParams };

            if (id.HasValue) view.Id = id.Value;
            if (tag != null) view.Tag = tag;
            if (gravity.HasValue) view.Gravity = gravity.Value;

            //order matters
            view.SetTextAppearance(context, GetTextAppearance(size));
            view.SetTextColor(color ?? Color.Black);
            view.Text = text;

            if (imeAction.HasValue)
            {
                view.SetSingleLine();
                view.ImeOptions = (ImeAction)imeAction;
            }

            //order matters
            if (password) view.InputType = InputTypes.ClassText | InputTypes.TextVariationPassword;

            return view;
        }

        public static Button Button(Context context, ViewGroup.LayoutParams layoutParams, XSizes size = XSizes.Medium, string text = null, Color? color = null, GravityFlags? gravity = null, int? id = null, string tag = null)
        {
            var view = new Button(context) { LayoutParameters = layoutParams };

            if (id.HasValue) view.Id = id.Value;
            if (tag != null) view.Tag = tag;
            if (gravity.HasValue) view.Gravity = gravity.Value;


            //order matters
            view.SetTextAppearance(context, GetTextAppearance(size));
            view.SetTextColor(color ?? Color.Black);
            view.Text = text;

            return view;
        }

        public static ImageView ImageView(Context context, ViewGroup.LayoutParams layoutParams, int srcId, Color? backgroundColor = null, int? id = null, string tag = null)
        {
            var view = new ImageView(context) { LayoutParameters = layoutParams };

            if (id.HasValue) view.Id = id.Value;
            if (tag != null) view.Tag = tag;

            view.SetImageResource(srcId);

            if (backgroundColor.HasValue) view.SetBackgroundColor(backgroundColor.Value);

            return view;
        }


        public static TextView Line(Context context, float width = XLayoutParams.MATCH, XSizes size = XSizes.Medium, Color? color = null, GravityFlags gravity = GravityFlags.NoGravity, int? id = null, string tag = null)
        {
            int height;

            switch (size)
            {
                case XSizes.Small:
                    height = 1;
                    break;
                case XSizes.Medium:
                    height = 2;
                    break;
                case XSizes.Large:
                    height = 3;
                    break;
                default:
                    throw new ParalibException("bad size");
            }

            var view = new TextView(context) { LayoutParameters = XLayoutParams.ViewGroup(width, height), Gravity = gravity };

            view.SetBackgroundColor(color ?? Color.Black);
            if (id.HasValue) view.Id = id.Value;
            if (tag != null) view.Tag = tag;

            return view;
        }


    }
}
