using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using PropertyChanged;
using Plugin.Share;

namespace OpenWeen.Forms.View
{
    [ImplementPropertyChanged]
    public partial class LoginDataPopup : PopupPage
    {
        public string AppID { get; set; }
        public string AppSecret { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; }
        public string PackageName { get; set; }

        public void OnAppIDChanged()
        {
            try
            {
                var data = Core.Helper.LoginDataHelper.Decode(AppID);
                AppID = data[0];
                AppSecret = data[1];
                RedirectUri = data[2];
                Scope = data[3];
                PackageName = data[4];
            }
            catch
            {
            }
        }

        public LoginDataPopup()
        {
            InitializeComponent();
            BindingContext = this;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            CrossShare.Current.SetClipboardText("SSMjExMTYwNjc5OjoxZTZlMzNkYjA4ZjkxOTIzMDZjNGFmYTBhNjFhZDU2Yzo6aHR0cDovL29hdXRoLndlaWNvLmNjOjplbWFpbCxkaXJlY3RfbWVzc2FnZXNfcmVhZCxkaXJlY3RfbWVzc2FnZXNfd3JpdGUsZnJpZW5kc2hpcHNfZ3JvdXBzX3JlYWQsZnJpZW5kc2hpcHNfZ3JvdXBzX3dyaXRlLHN0YXR1c2VzX3RvX21lX3JlYWQsZm9sbG93X2FwcF9vZmZpY2lhbF9taWNyb2Jsb2csaW52aXRhdGlvbl93cml0ZTo6Y29tLmVpY28ud2VpY286OkVFEE");
            Device.OpenUri(new Uri("https://gist.github.com/PeterCxy/3085799055f63c63c911"));
        }

        private async void StartLogin(object sender, EventArgs e)
        {
            RequestLogin?.Invoke($"https://api.weibo.com/oauth2/authorize?client_id={AppID}&response_type=token&display=mobile&redirect_uri={RedirectUri}&key_hash={AppSecret}{(string.IsNullOrEmpty(PackageName) ? "" : $"&packagename={PackageName}")}&scope={Scope}");
            await PopupNavigation.PopAsync();
        }
        protected override bool OnBackButtonPressed() => true;
        protected override bool OnBackgroundClicked() => true;
        public delegate void LoginUrlData(string url);
        public event LoginUrlData RequestLogin;
    }
}
