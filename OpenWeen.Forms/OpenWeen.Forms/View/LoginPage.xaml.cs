using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace OpenWeen.Forms.View
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var page = new LoginDataPopup();
            page.RequestLogin += Page_RequestLogin;
            await PopupNavigation.PushAsync(page);
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
            }
        }
    }
}
