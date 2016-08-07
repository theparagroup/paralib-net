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
        public static void Show(Context context, string title, string message, string okText = "Ok", EventHandler<DialogClickEventArgs> okHandler = null,string cancelText=null, EventHandler<DialogClickEventArgs> cancelHandler = null)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(context);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton(okText, okHandler?? ((senderAlert, args) => { }));

            if (cancelText != null)
            {
                alert.SetNegativeButton(cancelText, cancelHandler?? ((senderAlert, args) => { }));
            }

            Dialog dialog = alert.Create();
            dialog.Show();
        }


        public static void Popup(Context context, string text, bool @long=false)
        {
            Toast toast = Toast.MakeText(context, text,@long?ToastLength.Long:ToastLength.Short);
            toast.Show();
        }

    }



}