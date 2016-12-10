using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenWeen.Forms.Common.Controls;
using OpenWeen.Forms.UWP.Renderer;
using FormsPlugin.Iconize;
using FormsPlugin.Iconize.UWP;
using Plugin.Iconize.UWP;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ExIconNavigationPage), typeof(ExIconNavigationRenderer))]

namespace OpenWeen.Forms.UWP.Renderer
{
    public class ExIconNavigationRenderer : NavigationPageRenderer
    {
        private CommandBar _commandBar;

        public ExIconNavigationRenderer()
        {
            ElementChanged += ExIconNavigationRenderer_ElementChanged;
        }

        private void ContainerElement_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ContainerElement.Loaded -= ContainerElement_Loaded;
            _commandBar = typeof(PageControl).GetTypeInfo().GetDeclaredField("_commandBar").GetValue(ContainerElement) as CommandBar;
            _commandBar.DataContextChanged += CommandBar_DataContextChanged;
            SetToolbarItems((_commandBar.DataContext as global::Xamarin.Forms.Page).ToolbarItems);
        }

        private void CommandBar_DataContextChanged(Windows.UI.Xaml.FrameworkElement sender, Windows.UI.Xaml.DataContextChangedEventArgs args)
        {
            SetToolbarItems((args.NewValue as global::Xamarin.Forms.Page).ToolbarItems);
        }

        private void SetToolbarItems(IList<ToolbarItem> toolbarItems)
        {
            foreach (IconToolbarItem item in toolbarItems.Where(item => item is IconToolbarItem && (item as IconToolbarItem).IsVisible))
            {
                var element = _commandBar.PrimaryCommands.Where(command => command is AppBarButton && (command as AppBarButton).DataContext == item).FirstOrDefault();
                if (element == null) continue;
                var appBarButton = element as AppBarButton;
                var icon = Plugin.Iconize.Iconize.FindIconForKey(item.Icon);
                if (icon == null) continue;
                appBarButton.Icon = new FontIcon()
                {
                    FontFamily = Plugin.Iconize.Iconize.FindModuleOf(icon).ToFontFamily(),
                    Glyph = $"{icon.Character}",
                    Foreground = new SolidColorBrush(item.IconColor.ToWindowsColor()),
                };
            }
        }

        private void ExIconNavigationRenderer_ElementChanged(object sender, VisualElementChangedEventArgs e)
        {
            ElementChanged -= ExIconNavigationRenderer_ElementChanged;
            ContainerElement.Loaded += ContainerElement_Loaded;
        }
    }
}
