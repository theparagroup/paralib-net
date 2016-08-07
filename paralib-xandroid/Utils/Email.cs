using Android.App;
using Android.Content;
using Android.Telephony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Utils
{
    public class Email
    {

        public static void Send(Context context, string to, string cc=null, string subject = null, string text = null)
        {
            var email = new Intent(Intent.ActionSend);

            email.PutExtra(Intent.ExtraEmail, new string[] { to });

            if (cc!=null) email.PutExtra(Intent.ExtraCc, new string[] { cc });

            if (subject != null) email.PutExtra(Intent.ExtraSubject, subject);

            if (text != null) email.PutExtra(Intent.ExtraText,text);

            email.SetType("message/rfc822");

            context.StartActivity(email);
        }


    }
}
