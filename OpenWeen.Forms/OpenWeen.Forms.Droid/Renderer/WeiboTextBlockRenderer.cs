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
using OpenWeen.Forms.Common.Controls;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System.ComponentModel;
using Android.Text;
using Android.Text.Style;
using System.Text.RegularExpressions;
using OpenWeen.Forms.Common.Extension;
using OpenWeen.Forms.Droid.Common.Controls;

[assembly: ExportRenderer(typeof(WeiboTextBlock), typeof(OpenWeen.Forms.Droid.Renderer.WeiboTextBlockRenderer))]
namespace OpenWeen.Forms.Droid.Renderer
{
    public class WeiboTextBlockRenderer : ViewRenderer<WeiboTextBlock, TextView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WeiboTextBlock> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                SetNativeControl(new TextView(Context));
                
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(Element.Text):
                    TextChanged(Element.Text);
                    break;
                case nameof(Element.MaxLines):
                    Control.SetMaxLines(Element.MaxLines);
                    break;
                case nameof(Element.FontSize):
                    Control.TextSize = Convert.ToSingle(Element.FontSize);
                    break;
                default:
                    break;
            }
        }

        private void TextChanged(string text)
        {
            SpannableString span;
            var (isLongText, index) = WeiboTextBlock.CheckIsLongWeibo(text);
            if (isLongText)
            {
                var length = index + 2;
                text = text.Remove(index) + "х╚нд";
                span = new SpannableString(text);
                var colorSpan = new ForegroundColorSpan(Android.Graphics.Color.Blue);
                span.SetSpan(colorSpan, index, length, SpanTypes.ExclusiveExclusive);
            }
            else
            {
                span = new SpannableString(text);
            }
            var matches = Regex.Matches(text, WeiboTextBlock.REGEX).Cast<Match>();
            foreach (var item in matches)
            {
                var at = item.Groups[1];
                var topic = item.Groups[2];
                var emoji = item.Groups[3];
                var url = item.Groups[4];
                if (at.Success)
                {
                    var clickableSpan = new ExClickableSpan();
                    clickableSpan.OnClicked += (sender, e) =>
                    {
                        Element.InvokeUserClicked(at.Value.Replace("#", ""));
                    };
                    span.SetSpan(clickableSpan, at.Index, at.Index + at.Length, SpanTypes.ExclusiveExclusive);
                }
                if (topic.Success)
                {
                    var clickableSpan = new ExClickableSpan();
                    clickableSpan.OnClicked += (sender, e) =>
                    {
                        Element.InvokeTopicClicked(topic.Value.Replace("#", ""));
                    };
                    span.SetSpan(clickableSpan, topic.Index, topic.Index + topic.Length, SpanTypes.ExclusiveExclusive);
                }
                //if (emoji.Success && StaticResource.Emotions.Any(e => e.Value == emoji.Value))
                //{

                //}
                if (url.Success)
                {
                    var clickableSpan = new ExClickableSpan();
                    clickableSpan.OnClicked += (sender, e) =>
                    {
                        Element.InvokeTopicClicked(url.Value.Replace("#", ""));
                    };
                    span.SetSpan(clickableSpan, url.Index, url.Index + url.Length, SpanTypes.ExclusiveExclusive);
                }
            }
        }
    }
}