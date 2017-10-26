using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Widget;
using Android.Content;
using Android.Views;

namespace com.paralib.Xandroid.Widgets
{
    public class FrameLayoutWithFooter : FrameLayout
    {
        protected int _footerHeight = 0;

        public FrameLayoutWithFooter(Context context, int footerHeight) : base(context)
        {
            _footerHeight = footerHeight;
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            int parentHeight = MeasureSpec.GetSize(heightMeasureSpec);
            int myHeight = (int)(parentHeight - _footerHeight);
            base.OnMeasure(widthMeasureSpec, MeasureSpec.MakeMeasureSpec(myHeight, MeasureSpecMode.Exactly));
        }
    }
}
