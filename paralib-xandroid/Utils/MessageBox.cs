using Android.App;
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
        public static void Show(Context context, string title, string message, string okText = "Ok", EventHandler<DialogClickEventArgs> okHandler = null,string cancelText=null, EventHandler<DialogClickEventArgs> cancelHandler = null, bool modal=false)
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
        }

        private static Toast _lastToast;

        public static void Popup(Context context, string text, bool @long=false)
        {
            if (_lastToast!=null)
            {
                _lastToast.Cancel();
            }

            _lastToast = Toast.MakeText(context, text, @long?ToastLength.Long:ToastLength.Short);
            _lastToast.Show();
        }

    }



}
