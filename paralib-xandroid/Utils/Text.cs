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
            public bool? Underline { get; protected set; }
            public Color? Color { get; protected set; }

            public ClickableSpanWithAction(Action<View> onClick, bool? underline = null, Color? color = null)
            {
                _onClick = onClick;
                Underline = underline;
                Color = color;
            }

            public override void OnClick(View view)
            {
                if (_onClick != null)
                {
                    _onClick(view);
                }
            }

            public override void UpdateDrawState(TextPaint ds)
            {
                if (Underline.HasValue) ds.UnderlineText = Underline.Value;
                if (Color.HasValue) ds.Color = Color.Value;
            }

        }


        public static SpannableString SpannableText(string text, XSizes size, Color? color = null, Color? backgroundColor = null, bool underline = false, int? hangingIndent = null, EventHandler onClick = null)
        {

            var ss = new SpannableString(text ?? "");
            int l = text.Length;

            ss.SetSpan(new AbsoluteSizeSpan((int)XView.GetTextAppearance2(size), true), 0, l, SpanTypes.ExclusiveExclusive);

            if (underline) ss.SetSpan(new UnderlineSpan(), 0, l, SpanTypes.ExclusiveExclusive);

            if (backgroundColor.HasValue) ss.SetSpan(new BackgroundColorSpan(backgroundColor.Value), 0, l, SpanTypes.ExclusiveExclusive);

            if (hangingIndent.HasValue) ss.SetSpan(new LeadingMarginSpanStandard(0, hangingIndent.Value), 0, l, SpanTypes.ExclusiveExclusive);

            if (onClick != null)
            {
                var cs = new ClickableSpanWithAction(delegate (View view) { onClick(view, EventArgs.Empty); });
                ss.SetSpan(cs, 0, l, SpanTypes.ExclusiveExclusive);

                //make sure to set MovementMethod to Link
                //view.MovementMethod = new LinkMovementMethod();
            }

            if (color.HasValue) ss.SetSpan(new ForegroundColorSpan(color.Value), 0, l, SpanTypes.ExclusiveExclusive);

            return ss;

        }

        public static SpannableString SpannableText2(string lText, Color? lColor = null, string rText = "", Color? rColor = null, EventHandler onClick = null)
        {
            string text = lText + "\n" + rText;
            int l = text.Length;

            var ss = new SpannableString(text);

            ss.SetSpan(new AlignmentSpanStandard(Layout.Alignment.AlignNormal), 0, lText.Length, SpanTypes.ExclusiveExclusive);

            ss.SetSpan(new AlignmentSpanStandard(Layout.Alignment.AlignOpposite), l - rText.Length, l, SpanTypes.ExclusiveExclusive);

            if (onClick != null)
            {
                var cs = new ClickableSpanWithAction(delegate (View view) { onClick(view, EventArgs.Empty); }, false);
                ss.SetSpan(cs, 0, l, SpanTypes.ExclusiveExclusive);
            }

            if (lColor.HasValue) ss.SetSpan(new ForegroundColorSpan(lColor.Value), 0, lText.Length, SpanTypes.ExclusiveExclusive);
            if (rColor.HasValue) ss.SetSpan(new ForegroundColorSpan(rColor.Value), l - rText.Length, l, SpanTypes.ExclusiveExclusive);

            return ss;
        }


    }

}
