using System;
using System.Collections.Generic;
using System.Text;
#if WINDOWS_UWP
using FormsPlugin.Iconize.UWP;
using Windows.UI.Xaml;
#elif __IOS__
using FormsPlugin.Iconize.iOS;
using UIKit;
#else
using FormsPlugin.Iconize.Droid;
using Android.App;
#endif
namespace OpenWeen.Forms.Shared
{
    internal static class Initialization
    {
        public static void Init()
        {
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.MaterialModule());
            IconControls.Init();
        }
    }
}
