using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenWeen.Forms.Common.Helpers;
using OpenWeen.Forms.View;
using Xamarin.Forms;

namespace OpenWeen.Forms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //if (Settings.AccessTokens.Length == 0)
            //{
            //    SetMainPage(new LoginPage());
            //}
            //else
            //{
            //    SetMainPage(new MainPage());
            //}
            SetMainPage(new MainPage());
        }

        internal static void SetMainPage(Page page)
        {
            Current.MainPage = new Common.Controls.ExIconNavigationPage(page) { BarBackgroundColor = (Color)Current.Resources["AppTheme"], BarTextColor = Color.White, };
        }
        
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
