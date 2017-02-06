using OpenWeen.Forms.Common.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using Foundation;
using System.Text.RegularExpressions;
using System.Linq;

[assembly: ExportRenderer(typeof(WeiboTextBlock), typeof(OpenWeen.Forms.iOS.Renderer.WeiboTextBlockRenderer))]
namespace OpenWeen.Forms.iOS.Renderer
{
    public class WeiboTextBlockRenderer : ViewRenderer<WeiboTextBlock, UITextView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WeiboTextBlock> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new UITextView() { Editable = false });
            }
            if (e.OldElement != null)
            {
                Control.ShouldInteractWithUrl -= ShouldInteractWithUrl;
            }
            if (e.NewElement != null)
            {
                Control.ShouldInteractWithUrl += ShouldInteractWithUrl;
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
                    break;
                case nameof(Element.FontSize):
                    TextChanged(Element.Text);
                    break;
                default:
                    break;
            }
        }

        private void TextChanged(string text)
        {
            NSMutableAttributedString attributedText;
            var (isLongText, index) = WeiboTextBlock.CheckIsLongWeibo(text);
            if (isLongText)
            {
                var length = index + 2;
                text = text.Remove(index) + "全文";
                attributedText = new NSMutableAttributedString(text, font: UIFont.SystemFontOfSize(Convert.ToSingle(Element.FontSize)));
                attributedText.AddAttribute(UIStringAttributeKey.ForegroundColor, UIColor.Blue , new NSRange(index, 2));
            }
            else
            {
                attributedText = new NSMutableAttributedString(text, font: UIFont.SystemFontOfSize(Convert.ToSingle(Element.FontSize)));
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
                    attributedText.AddAttribute(UIStringAttributeKey.Link, new NSString($"at://{at.Value.Replace("@", "")}"), new NSRange(at.Index, at.Length));
                }
                if (topic.Success)
                {
                    attributedText.AddAttribute(UIStringAttributeKey.Link, new NSString($"topic://{topic.Value}"), new NSRange(topic.Index, topic.Length));
                }
                //if (emoji.Success && StaticResource.Emotions.Any(e => e.Value == emoji.Value))
                //{

                //}
                if (url.Success)
                {
                    attributedText.AddAttribute(UIStringAttributeKey.Link, new NSString(url.Value), new NSRange(url.Index, url.Length));
                }
            }
        }

        private bool ShouldInteractWithUrl(UITextView arg1, NSUrl arg2, NSRange arg3)
        {
            switch (arg2.Scheme)
            {
                case "at":
                    Element.InvokeUserClicked(arg2.Host);
                    return false;
                case "topic":
                    Element.InvokeTopicClicked(arg2.Host);
                    return false;
                default:
                    Element.InvokeLinkClicked(arg2.ToString());
                    return false;
            }
        }

    }
}
