using Android.App;
using Android.Content.Res;
using Android.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Utils
{
    public class Attributes
    {
        private TypedArray _typeArray;

        public Attributes(IAttributeSet set, int[] attrs)
        {
            _typeArray = Application.Context.ObtainStyledAttributes(set,attrs);
        }

        public string GetString(int index)
        {
            return _typeArray.GetString(index);
        }

        public float GetDimension(int index, float @default)
        {
            return _typeArray.GetDimension(index, @default);
        }
    }
}
