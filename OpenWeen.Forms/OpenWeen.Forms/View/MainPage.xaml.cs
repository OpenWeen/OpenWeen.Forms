using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeen.Forms.ViewModel.MainPage;
using Xamarin.Forms;

namespace OpenWeen.Forms.View
{
    public partial class MainPage : ContentPage
    {
        //public MainViewModel ViewModel { get; } = new MainViewModel();
        public MainPage()
        {
            InitializeComponent();
            //BindingContext = ViewModel;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listview.ItemsSource = (await Core.Api.Statuses.Home.GetTimeline()).Statuses;
        }
    }
}
