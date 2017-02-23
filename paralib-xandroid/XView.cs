using Android.Content;
using Android.Content.Res;
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
        public static int GetTextAppearanceX(XSizes size)
        {
            switch (size)
            {
                case XSizes.Small:
                    return Android.Resource.Style.TextAppearanceSmall; //14sp
                case XSizes.Medium:
                    return Android.Resource.Style.TextAppearanceMedium; //18sp
                case XSizes.Large:
                    return Android.Resource.Style.TextAppearanceLarge; //22sp
                default:
                    throw new ParalibException("bad size");
            }


        }

        public static float GetTextAppearance2(XSizes size)
        {
            switch (size)
            {
                case XSizes.Small:
                    return 14;
                case XSizes.Medium:
                    return 18;
                case XSizes.Large:
                    return 22;
                case XSizes.XLarge:
                    return 26;
                default:
                    throw new ParalibException("bad size");
            }


        }

        public static TextView TextView(Context context, XSizes size, string text = null, Color? color = null, Color? backgroundColor = null, int? id = null, string tag = null)
        {
            var view = new TextView(context);
            view.Text = text;

            if (id.HasValue) view.Id = id.Value;
            if (tag != null) view.Tag = tag;

            //order matters (do this before other appearance changes)
            //view.SetTextAppearance(context, GetTextAppearance(size));
            view.TextSize = GetTextAppearance2(size);
            view.SetTextColor(color ?? Color.Black);
            if (backgroundColor != null) view.SetBackgroundColor(backgroundColor.Value);

            return view;
        }

        public static TextView TextView(Context context, ViewGroup.LayoutParams layoutParams, XSizes size = XSizes.Medium, string text = null, Color? color = null, Color? backgroundColor = null, GravityFlags? textGravity = null, int? id = null, string tag = null)
        {
            var view = TextView(context, size, text, color, backgroundColor, id, tag);

            view.LayoutParameters = layoutParams;

            if (textGravity.HasValue) view.Gravity = textGravity.Value;

            return view;
        }

        public static Action<Context, EditText> EditTextStyling;


        public static EditText EditText(Context context, ViewGroup.LayoutParams layoutParams, XSizes size = XSizes.Medium, string text = null, Color? color = null, XInputTypes inputType = XInputTypes.Text, XImeActions? imeAction = null, GravityFlags? textGravity = null, int? id = null, string tag = null, bool? selectOnFocus = false, int? maxLength=null, EventHandler onTextChanged = null)
        {
            var view = new EditText(context) { LayoutParameters = layoutParams };

            if (id.HasValue) view.Id = id.Value;
            if (tag != null) view.Tag = tag;
            if (textGravity.HasValue) view.Gravity = textGravity.Value;
            if (selectOnFocus.HasValue) view.SetSelectAllOnFocus(selectOnFocus.Value);

            //order matters
            //view.SetTextAppearance(context, GetTextAppearance(size));
            view.TextSize = GetTextAppearance2(size);

            view.SetTextColor(color ?? Color.Black);
            view.Text = text;

            if (imeAction.HasValue)
            {
                //this is required for ime actions, but now the box won't strech horizontally
                //
                //  this doesn't help:
                //      view.SetHorizontallyScrolling(false);

                view.SetSingleLine();
                view.ImeOptions = (ImeAction)imeAction;
            }


            //order matters
            if (inputType == XInputTypes.Password)
            {
                view.InputType = InputTypes.ClassText | InputTypes.TextVariationPassword;
            }
            else if (inputType == XInputTypes.Decimal)
            {
                view.InputType = InputTypes.ClassNumber | InputTypes.NumberFlagDecimal;
            }

            if (maxLength.HasValue)
            {
                view.SetFilters(new IInputFilter[] { new InputFilterLengthFilter(maxLength.Value) });
            }

            if (EditTextStyling != null)
            {
                EditTextStyling(context, view);
            }

            //text is already set so the first time through doesn't fire the event
            if (onTextChanged != null) view.TextChanged += (senderAlert, args) => { onTextChanged(senderAlert, args); };

            return view;
        }

        public static Action<Context, Button> ButtonStyling;

        public static Button Button(Context context, ViewGroup.LayoutParams layoutParams, XSizes size = XSizes.Medium, string text = null, Color? color = null, GravityFlags? textGravity = null, int? id = null, string tag = null)
        {
            var view = new Button(context) { LayoutParameters = layoutParams };

            if (id.HasValue) view.Id = id.Value;
            if (tag != null) view.Tag = tag;
            if (textGravity.HasValue) view.Gravity = textGravity.Value;


            //order matters
            //view.SetTextAppearance(context, GetTextAppearance(size));
            view.TextSize = GetTextAppearance2(size);

            view.SetTextColor(color ?? Color.Black);
            view.Text = text;

            if (ButtonStyling!=null)
            {
                ButtonStyling(context, view);
            }

            return view;
        }

        public static ImageView ImageView(Context context, ViewGroup.LayoutParams layoutParams, int? srcId = null, Color? backgroundColor = null, int? id = null, string tag = null)
        {
            var view = new ImageView(context) { LayoutParameters = layoutParams };

            if (id.HasValue) view.Id = id.Value;
            if (tag != null) view.Tag = tag;


            view.SetAdjustViewBounds(true);

            if (srcId != null) view.SetImageResource(srcId.Value);

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

        public static Action<Context, Spinner> SpinnerStyling;
        public static Action<TextView> SpinnerButtonStyling;


        private class _AA : ArrayAdapter
        {
            public _AA(Context context, int resource, int textViewResourceId, System.Collections.IList objects) : base(context, resource, textViewResourceId, objects)
            {

            }

            public override View GetDropDownView(int position, View convertView, ViewGroup parent)
            {
                //return view for item in drop downlist
                //convertView==reuse view
                //parent==view to attach new view to

                //parent.SetBackgroundColor(Color.Purple);


                //force new view to be created
                var view = base.GetDropDownView(position, convertView, parent);
                return view;
            }


            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var view = base.GetView(position, convertView, parent);

                if (view is TextView)
                {
                    SpinnerButtonStyling((TextView)view);
                }


                return view;
            }


        }

        public static Spinner Spinner<T>(Context context, List<T> items, int? id = null, Action<Spinner, int, T> onItemSelected = null) where T :class
        {
            Spinner spinner = new Spinner(context, SpinnerMode.Dialog);

            //this will create the StateListDrawable with the NinePatches loaded (which is the default):
            //  spinner.SetBackgroundDrawable(context.Resources.GetDrawable(Android.Resource.Drawable.ButtonDropDown));

            if (id != null) spinner.Id = id.Value;

            ArrayAdapter adapterFrom = new _AA(context, Android.Resource.Layout.SimpleSpinnerItem, Android.Resource.Id.Text1, items); 
            adapterFrom.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapterFrom;

            if (onItemSelected != null)
            {

                spinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                    var daSpinner = (Spinner)sender;

                    var jo = spinner.SelectedItem;
                    var propertyInfo = jo.GetType().GetProperty("Instance");
                    var si = propertyInfo == null ? default(T) : propertyInfo.GetValue(jo, null) as T;
                    var pos = daSpinner.SelectedItemPosition;

                    onItemSelected(spinner, pos, si);

                };
            }

            if (SpinnerStyling != null)
            {
                SpinnerStyling(context, spinner);
            }

            return spinner;

        }
    }
}
