using OpenWeen.Forms.Common.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;
using System.ComponentModel;
using OpenWeen.Core.Model.Status;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Markup;
using OpenWeen.Forms.Common.Extension;

[assembly: ExportRenderer(typeof(WeiboTextBlock), typeof(OpenWeen.Forms.UWP.Renderer.WeiboTextBlockRenderer))]
namespace OpenWeen.Forms.UWP.Renderer
{
    public class WeiboTextBlockRenderer : ViewRenderer<WeiboTextBlock, RichTextBlock>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WeiboTextBlock> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                SetNativeControl(new RichTextBlock());
            }
            if (e.OldElement != null)
            {
                Control.Tapped -= Control_Tapped;
                Control.RightTapped -= Control_RightTapped;
                Control.Holding -= Control_Holding;
            }
            if (e.NewElement != null)
            {
                Control.Tapped += Control_Tapped;
                Control.RightTapped += Control_RightTapped;
                Control.Holding += Control_Holding;
                Control.IsTextSelectionEnabled = true;
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
                    Control.MaxLines = Element.MaxLines;
                    break;
                case nameof(Element.FontSize):
                    Control.FontSize = Element.FontSize;
                    break;
                default:
                    break;
            }
        }
        private void TextChanged(string text)
        {
            //string text = "";
            //var model = Element.BindingContext as MessageModel;
            var oritext = text;
            //string ortext = (model?.LongText != null) ? model.LongText.Content : Element.Text;
            text = text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&apos;").Replace("\"", "&quot;").Replace("\n", "<LineBreak/>");
            var index = Math.Max(text.IndexOf("全文： http://m.weibo.cn/"), text.IndexOf("http://m.weibo.cn/client/version"));
            if (index != -1)
            {
                text = text.Remove(index);
                text += @"<InlineUIContainer><TextBlock Foreground=""{ThemeResource HyperlinkForegroundThemeBrush}"">全文</TextBlock></InlineUIContainer>";
            }
            var matches = Regex.Matches(text, WeiboTextBlock.REGEX).Cast<Match>().Distinct((item => item.Value));
            foreach (var item in matches)
            {
                var at = item.Groups[1];
                var topic = item.Groups[2];
                var emoji = item.Groups[3];
                var url = item.Groups[4];
                if (at.Success)
                {
                    text = text.Replace(at.Value, @"<InlineUIContainer><TextBlock Foreground=""{ThemeResource HyperlinkForegroundThemeBrush}"">" + at.Value + "</TextBlock></InlineUIContainer>");
                }
                if (topic.Success)
                {
                    text = text.Replace(topic.Value, @"<InlineUIContainer><TextBlock Foreground=""{ThemeResource HyperlinkForegroundThemeBrush}"">" + topic.Value + "</TextBlock></InlineUIContainer>");
                }
                //if (emoji.Success && StaticResource.Emotions.Any(e => e.Value == emoji.Value))
                //{
                //    text = text.Replace(emoji.Value, $@"<InlineUIContainer><Image Source=""{StaticResource.Emotions.FirstOrDefault(e => e.Value == emoji.Value).Url}"" Width=""15"" Height=""15""/></InlineUIContainer>");
                //}
                if (url.Success)
                {
                    text = text.Replace(url.Value, "<InlineUIContainer><TextBlock Foreground=\"{ThemeResource HyperlinkForegroundThemeBrush}\" Tag=\"" + url.Value + "\">网页链接</TextBlock></InlineUIContainer>");
                }
            }
            try
            {
                AddBlockFromText(text);
            }
            catch (Exception)
            {
                AddBlockFromText(oritext.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&apos;").Replace("\"", "&quot;").Replace("\n", "<LineBreak/>"));
            }
        }

        private void AddBlockFromText(string text)
        {
            var xaml = string.Format(@"<Paragraph
                                        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                                        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"" >
                                    <Paragraph.Inlines><Run></Run>{0}</Paragraph.Inlines>
                                    </Paragraph>", text);
            Control.Blocks.Clear();
            Control.Blocks.Add((Paragraph) XamlReader.Load(xaml));
        }

        private bool CheckIsTopic(string text) => Regex.IsMatch(text, WeiboTextBlock.TOPIC);

        private bool CheckIsUserName(string text) => Regex.IsMatch(text, WeiboTextBlock.AT);


        private void Control_Holding(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            if ((e.OriginalSource as TextBlock)?.Tag != null)
                Element.LinkHolding((e.OriginalSource as TextBlock)?.Tag.ToString());
        }

        private void Control_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            if ((e.OriginalSource as TextBlock)?.Tag != null)
                Element.LinkHolding((e.OriginalSource as TextBlock)?.Tag.ToString());
        }

        private void Control_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (e.OriginalSource is TextBlock)
            {
                var text = (e.OriginalSource as TextBlock).Text;
                if (CheckIsTopic(text))
                {
                    e.Handled = true;
                    Element.InvokeTopicClicked(text.Replace("#", ""));
                }
                else if (CheckIsUserName(text))
                {
                    e.Handled = true;
                    Element.InvokeUserClicked(text.Replace("@", ""));
                }
                else if ((e.OriginalSource as TextBlock).Tag != null)
                {
                    e.Handled = true;
                    Element.InvokeLinkClicked((e.OriginalSource as TextBlock).Tag.ToString());
                }
            }
        }
    }
}
