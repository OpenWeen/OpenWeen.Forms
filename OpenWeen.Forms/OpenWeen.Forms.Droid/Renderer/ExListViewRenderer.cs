using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OpenWeen.Forms.Common.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(ExListView), typeof(OpenWeen.Forms.Droid.Renderer.ExListViewRenderer))]
namespace OpenWeen.Forms.Droid.Renderer
{
    public class ExListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.ItemLongClick += (sender, ee) =>
                {
                    var items = Element.ItemsSource as IList;
                    if (items != null)
                        (Element as ExListView).OnLongPress(items[ee.Position]);
                };
            }
        }
    }
}