using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OpenWeen.Forms.Common.Controls
{
    public class WeiboTextBlock : ContentView
    {
        public const string AT = @"@[^,，：:\s@]+";
        public const string TOPIC = "#[^#]+#";
        public const string EMOJI = "\\[[\\w]+\\]";
        public const string URL = "http://[-a-zA-Z0-9+&@#/%?=~_|!:,.;]*[-a-zA-Z0-9+&@#/%=~_|]";
        public static readonly string REGEX = $"({AT})|({TOPIC})|({EMOJI})|({URL})";

        public event WeiboTextBlockClickDataEventArgs UserClick;
        public event WeiboTextBlockClickDataEventArgs TopicClick;
        public event WeiboTextBlockClickDataEventArgs LinkClick;
        public delegate void WeiboTextBlockClickDataEventArgs(string data);

        public void InvokeUserClicked(string userName)
        {
            UserClick?.Invoke(userName);
        }

        public void InvokeTopicClicked(string topic)
        {
            TopicClick?.Invoke(topic);
        }

        public void InvokeLinkClicked(string link)
        {
            LinkClick?.Invoke(link);
        }

        public void LinkHolding(string link)
        {

        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(WeiboTextBlock), null);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
       
        //public static readonly BindableProperty IsTextSelectionEnableProperty = BindableProperty.Create(nameof(IsTextSelectionEnable), typeof(bool), typeof(WeiboTextBlock), false);

        //public bool IsTextSelectionEnable
        //{
        //    get { return (bool)GetValue(IsTextSelectionEnableProperty); }
        //    set { SetValue(IsTextSelectionEnableProperty, value); }
        //}

        public static (bool, int) CheckIsLongWeibo(string text)
        {
            var index = Math.Max(text.IndexOf("全文： http://m.weibo.cn/"), text.IndexOf("http://m.weibo.cn/client/version"));
            return (index != -1, index);
        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(WeiboTextBlock), 0d);

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty MaxLinesProperty = BindableProperty.Create(nameof(MaxLines), typeof(int), typeof(WeiboTextBlock), 0);

        public int MaxLines
        {
            get { return (int)GetValue(MaxLinesProperty); }
            set { SetValue(MaxLinesProperty, value); }
        }
    }
}
