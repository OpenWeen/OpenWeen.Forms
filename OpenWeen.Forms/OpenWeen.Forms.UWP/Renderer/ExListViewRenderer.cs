using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeen.Forms.Common.Controls;
using Windows.UI.Xaml;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ExListView), typeof(OpenWeen.Forms.UWP.Renderer.ExListViewRenderer))]

namespace OpenWeen.Forms.UWP.Renderer
{
    public class ExListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Holding += (sender, ee) => (Element as ExListView).OnLongPress((ee.OriginalSource as FrameworkElement).DataContext is ViewCell ? (((ee.OriginalSource as FrameworkElement).DataContext) as ViewCell).BindingContext : (ee.OriginalSource as FrameworkElement).DataContext);
                Control.RightTapped += (sender, ee) => (Element as ExListView).OnLongPress((ee.OriginalSource as FrameworkElement).DataContext is ViewCell ? (((ee.OriginalSource as FrameworkElement).DataContext) as ViewCell).BindingContext : (ee.OriginalSource as FrameworkElement).DataContext);
            }
        }
    }
}
