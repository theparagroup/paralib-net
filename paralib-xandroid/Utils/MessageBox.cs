using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Utils
{
    public class MessageBox
    {
        public static void Show(Context context, string title, string message, string okText="Ok")
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(context);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton(okText, (senderAlert, args) => { });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

    }
}
