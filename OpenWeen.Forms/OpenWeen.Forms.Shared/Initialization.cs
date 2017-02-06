using System;
using System.Collections.Generic;
using System.Text;
#if WINDOWS_UWP
using FormsPlugin.Iconize.UWP;
using ImageCircle.Forms.Plugin.UWP;
using FFImageLoading.Forms.WinUWP;
using Windows.UI.Xaml;
#elif __IOS__
using FormsPlugin.Iconize.iOS;
using ImageCircle.Forms.Plugin.iOS;
using UIKit;
using FFImageLoading.Forms.Touch;
#else
using FormsPlugin.Iconize.Droid;
using ImageCircle.Forms.Plugin.Droid;
using Android.App;
using FFImageLoading.Forms.Droid;
#endif
namespace OpenWeen.Forms.Shared
{
    internal static class Initialization
    {
        public static void Init()
        {
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.MaterialModule());
            IconControls.Init();
            ImageCircleRenderer.Init();
            CachedImageRenderer.Init();
        }
    }
}
