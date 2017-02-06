using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using OpenWeen.Forms.Common.Helpers;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace OpenWeen.Forms.View
{
    public partial class LoginPage : ContentPage
    {
        private LoginDataPopup _popup;

        public LoginPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_popup == null)
            {
                _popup = new LoginDataPopup();
                _popup.RequestLogin += Page_RequestLogin;
                await PopupNavigation.PushAsync(_popup);
            }
        }

        private void Page_RequestLogin(string url)
        {
            webView.Source = new UrlWebViewSource() { Url = url };
        }

        private void webView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (!e.Url.Contains("error") && e.Url.Contains("access_token="))
            {
                var regex = Regex.Match(e.Url, "access_token=(.*)\\&remind_in=([0-9]*)");
                var token = regex.Groups[1].Value;
                Settings.AccessTokens = new[] { token }.Concat(Settings.AccessTokens).ToArray();
                App.SetMainPage(new MainPage());
            }
        }
    }
}
