using System;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Text.Method;

namespace com.paralib.Xandroid.Utils
{
    public class Text
    {
        public class ClickableSpanWithAction : ClickableSpan
        {
            protected Action<View> _onClick;

            public ClickableSpanWithAction(Action<View> onClick)
            {
                _onClick = onClick;
            }

            public override void OnClick(View view)
            {
                if (_onClick != null)
                {
                    _onClick(view);
                }
            }

        }


        public static SpannableString SpannableText(string text, XSizes size, Color? color = null, Color? backgroundColor = null, bool underline = false, int? hangingIndent = null, int? id = null, string tag = null, EventHandler onClick = null)
        {

            var ss = new SpannableString(text ?? "");
            int l = text.Length;

            ss.SetSpan(new AbsoluteSizeSpan((int)XView.GetTextAppearance2(size), true), 0, l, SpanTypes.InclusiveInclusive);

            if (underline) ss.SetSpan(new UnderlineSpan(), 0, l, SpanTypes.InclusiveInclusive);

            if (backgroundColor.HasValue) ss.SetSpan(new BackgroundColorSpan(backgroundColor.Value), 0, l, SpanTypes.InclusiveInclusive);

            if (hangingIndent.HasValue) ss.SetSpan(new LeadingMarginSpanStandard(0, hangingIndent.Value), 0, l, SpanTypes.InclusiveInclusive);

            if (onClick != null)
            {
                var cs = new ClickableSpanWithAction(delegate (View view) { onClick(view, EventArgs.Empty); });
                ss.SetSpan(cs, 0, l, SpanTypes.InclusiveInclusive);

                //make sure to set MovementMethod to Link
                //view.MovementMethod = new LinkMovementMethod();
            }

            if (color.HasValue) ss.SetSpan(new ForegroundColorSpan(color.Value), 0, l, SpanTypes.InclusiveInclusive);

            return ss;
        }


    }

}
