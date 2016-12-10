using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using OpenWeen.Forms.Common.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExListView), typeof(OpenWeen.Forms.iOS.Renderer.ExListViewRenderer))]


namespace OpenWeen.Forms.iOS.Renderer
{
    public class ExListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            var gr = new UILongPressGestureRecognizer(o =>
            {
                if (o.State == UIGestureRecognizerState.Began)
                {
                    var p = o.LocationInView(Control);
                    var indexPath = Control.IndexPathForRowAtPoint(p);
                    var items = Element.ItemsSource as IList;
                    if (items != null)
                        (Element as ExListView).OnLongPress(items[indexPath.Row]);
                }
            });
            AddGestureRecognizer(gr);
        }
    }
}