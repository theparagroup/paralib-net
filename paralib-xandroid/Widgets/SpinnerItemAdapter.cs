using Android.Content;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Widgets
{
    public class SpinnerItemAdapter : ArrayAdapter
    {

        protected Action<TextView> _spinnerButtonStyling;
        protected Action<TextView> _spinnerDropDownStyling;

        public SpinnerItemAdapter(Context context, int resource, int textViewResourceId, List<ISpinnerItem> objects, Action<TextView> spinnerButtonStyling=null, Action<TextView> spinnerDropDownStyling = null) : base(context, resource, textViewResourceId, objects)
        {
            _spinnerButtonStyling = spinnerButtonStyling;
            _spinnerDropDownStyling = spinnerDropDownStyling;
        }

        public ISpinnerItem GetSpinnerItem(int position)
        {
            var jo = GetItem(position);
            var propertyInfo = jo.GetType().GetProperty("Instance");
            var si = propertyInfo == null ? null : propertyInfo.GetValue(jo, null) as ISpinnerItem;
            return si;
        }


        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            //return view for item in drop downlist
            //convertView==reuse view
            //parent==view to attach new view to

            //parent.SetBackgroundColor(Color.Purple);


            //force new view to be created
            var view = base.GetDropDownView(position, convertView, parent);

            if (view is TextView)
            {
                var spinnerItem = GetSpinnerItem(position);
                if (spinnerItem != null) ((TextView)view).Text = spinnerItem.DropdownDisplay ?? spinnerItem.Display ?? spinnerItem.Key;

                if (_spinnerDropDownStyling != null)
                {
                    _spinnerDropDownStyling((TextView)view);
                }

            }

            return view;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = base.GetView(position, convertView, parent);

            if (view is TextView)
            {
                var spinnerItem = GetSpinnerItem(position);
                if (spinnerItem!=null) ((TextView)view).Text = spinnerItem.ButtonDisplay ?? spinnerItem.Display ?? spinnerItem.Key;

                if (_spinnerButtonStyling != null)
                {
                    _spinnerButtonStyling((TextView)view);
                }
            }


            return view;
        }


    }
}
