using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeen.Forms.Common.Controls;
using OpenWeen.Forms.UWP.Renderer;
using FormsPlugin.Iconize;
using FormsPlugin.Iconize.UWP;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Plugin.Iconize.UWP;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ExIconImage), typeof(ExIconImageRenderer))]
namespace OpenWeen.Forms.UWP.Renderer
{
    public class ExIconImageRenderer : IconImageRenderer
    {
        protected override async void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (Control == null || Element == null)
                return;
            await UpdateImage();
        }
        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control == null || Element == null)
                return;

            switch (e.PropertyName)
            {
                case nameof(IconImage.Icon):
                case nameof(IconImage.IconColor):
                case nameof(IconImage.IconSize):
                    await UpdateImage();
                    break;
            }
        }

        private async Task UpdateImage()
        {
            var iconImage = Element as IconImage;
            var icon = Plugin.Iconize.Iconize.FindIconForKey(iconImage.Icon);
            CanvasDevice device = CanvasDevice.GetSharedDevice();
            var target = new CanvasRenderTarget(device, Convert.ToSingle(Element.HeightRequest), Convert.ToSingle(Element.HeightRequest), 96 * 4);
            using (var session = target.CreateDrawingSession())
            using (var format = new CanvasTextFormat { FontSize = Convert.ToSingle(Element.HeightRequest), FontFamily = Plugin.Iconize.Iconize.FindModuleOf(icon).ToFontFamily().Source })
            using (var textLayout = new CanvasTextLayout(device, $"{icon.Character}", format, Convert.ToSingle(Element.HeightRequest), Convert.ToSingle(Element.HeightRequest)))
                session.DrawTextLayout(textLayout, 0, 0, iconImage.IconColor.ToWindowsColor());
            using (var stream = new InMemoryRandomAccessStream())
            {
                await target.SaveAsync(stream, CanvasBitmapFileFormat.Png);
                stream.Seek(0);
                BitmapImage result = new BitmapImage();
                await result.SetSourceAsync(stream);
                Control.Source = result;
            }
        }

    }
}
