using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Text.Style;
using Android.Text;

namespace OpenWeen.Forms.Droid.Common.Controls
{
    internal class ExClickableSpan : ClickableSpan
    {
        public event EventHandler OnClicked;
        public override void OnClick(Android.Views.View widget)
        {
            OnClicked?.Invoke(this, null);
        }

        public override void UpdateDrawState(TextPaint ds)
        {
            base.UpdateDrawState(ds);
            ds.Color = Android.Graphics.Color.Blue;
            ds.UnderlineText = false;
        }
    }
}