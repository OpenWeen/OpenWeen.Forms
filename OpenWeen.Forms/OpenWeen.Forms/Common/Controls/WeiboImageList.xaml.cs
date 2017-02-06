using FFImageLoading.Forms;
using OpenWeen.Core.Model.Status;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OpenWeen.Forms.Common.Controls
{
    public partial class WeiboImageList : ContentView
    {
        public WeiboImageList()
        {
            InitializeComponent();
        }

        public IList<PictureModel> ItemsSource
        {
            get { return (IList<PictureModel>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList<PictureModel>), typeof(WeiboImageList), propertyChanged: OnItemsSourceChanged);

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as WeiboImageList).OnItemsSourceChanged(newValue as IList<PictureModel>);
        }

        private void OnItemsSourceChanged(IList<PictureModel> list)
        {
            foreach (var item in root.Children)
                item.IsVisible = false;
            Grid grid;
            switch (list.Count)
            {
                case 0:
                    return;
                case 1:
                    grid = OneGrid;
                    break;
                case 2:
                    grid = TwoGrid;
                    break;
                case 3:
                    grid = ThreeGrid;
                    break;
                case 4:
                    grid = FourGird;
                    break;
                case 5:
                case 6:
                    grid = SixGrid;
                    break;
                case 7:
                case 8:
                case 9:
                default:
                    grid = NineGrid;
                    break;
            }
            foreach (CachedImage item in grid.Children)
                item.Source = null;
            grid.IsVisible = true;
            for (int i = 0; i < Math.Min(9, list.Count); i++)
            {
                var item = list[i];
                var img = grid.Children[i] as CachedImage;
                img.Source = new UriImageSource() { Uri = new Uri(GetImage(item)) };
            }
        }

        private string GetImage(PictureModel item) => CrossConnectivity.Current.ConnectionTypes.Any(type => type == ConnectionType.Cellular) ? item.ThumbnailPic : item.ToBmiddle;
                 
    }
}
