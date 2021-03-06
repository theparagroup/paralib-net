﻿using Android.App;
using Android.Content;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Utils
{
    public class MessageBox
    {
        public static void Show(Context context, string title, string message, string okText = "Ok", EventHandler<DialogClickEventArgs> okHandler = null,string cancelText=null, EventHandler<DialogClickEventArgs> cancelHandler = null, bool modal=false, float? scale=null, int? width=null, int? height=null)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(context);

            if (scale.HasValue)
            {
                var titleSpan = new Android.Text.SpannableString(title);
                titleSpan.SetSpan(new Android.Text.Style.RelativeSizeSpan(scale.Value), 0, titleSpan.Length(), 0);
                alert.SetTitle(titleSpan);

                var messageSpan = new Android.Text.SpannableString(message);
                messageSpan.SetSpan(new Android.Text.Style.RelativeSizeSpan(scale.Value), 0, messageSpan.Length(), 0);
                alert.SetMessage(messageSpan);
            }
            else
            {
                alert.SetTitle(title);
                alert.SetMessage(message);
            }

            alert.SetPositiveButton(okText, okHandler?? ((senderAlert, args) => { }));

            if (cancelText != null)
            {
                alert.SetNegativeButton(cancelText, cancelHandler?? ((senderAlert, args) => { }));
            }

            Dialog dialog = alert.Create();

            if (modal)
            {
                dialog.SetCancelable(false);
                dialog.SetCanceledOnTouchOutside(false);
            }

            if (_lastToast != null)
            {
                _lastToast.Cancel();
                _lastToast = null;
            }


            dialog.Show();

            if (width.HasValue && height.HasValue)
            {
                dialog.Window.SetLayout(width.Value, height.Value);
            }
        }

        private static Toast _lastToast;

        public static void Popup(Context context, string text, bool @long=false, float? scale=null)
        {
            if (_lastToast!=null)
            {
                _lastToast.Cancel();
            }

            if (scale.HasValue)
            {
                var popupSpan = new Android.Text.SpannableString(text);
                popupSpan.SetSpan(new Android.Text.Style.RelativeSizeSpan(scale.Value), 0, popupSpan.Length(), 0);
                _lastToast = Toast.MakeText(context, popupSpan, @long ? ToastLength.Long : ToastLength.Short);
            }
            else
            {
                _lastToast = Toast.MakeText(context, text, @long ? ToastLength.Long : ToastLength.Short);
            }

            _lastToast.Show();
        }

    }



}
