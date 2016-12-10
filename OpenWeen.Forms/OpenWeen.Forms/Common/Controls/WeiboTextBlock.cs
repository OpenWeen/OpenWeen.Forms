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
        public const string TOPIC_PATTERN = "#[^#]+#";
        public const string USERNAME_PATTERN = @"@[^,，：:\s@]+";
        public const string URL_PATTERN = "http://[-a-zA-Z0-9+&@#/%?=~_|!:,.;]*[-a-zA-Z0-9+&@#/%=~_|]";

        public event WeiboTextBlockClickDataEventArgs UserClick;
        public event WeiboTextBlockClickDataEventArgs TopicClick;
        public delegate void WeiboTextBlockClickDataEventArgs(string data);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(WeiboTextBlock), null, propertyChanged: OnTextChanged);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as WeiboTextBlock).TextChanged();
        }

        public static readonly BindableProperty IsTextSelectionEnableProperty = BindableProperty.Create(nameof(IsTextSelectionEnable), typeof(bool), typeof(WeiboTextBlock), false);

        public bool IsTextSelectionEnable
        {
            get { return (bool)GetValue(IsTextSelectionEnableProperty); }
            set { SetValue(IsTextSelectionEnableProperty, value); }
        }

        public static readonly BindableProperty MaxLinesProperty = BindableProperty.Create(nameof(MaxLines), typeof(int), typeof(WeiboTextBlock), 0);

        public int MaxLines
        {
            get { return (int)GetValue(MaxLinesProperty); }
            set { SetValue(MaxLinesProperty, value); }
        }

        private void TextChanged()
        {
            throw new NotImplementedException();
        }
    }
}
