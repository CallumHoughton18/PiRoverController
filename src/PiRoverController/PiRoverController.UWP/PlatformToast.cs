using PiRoverController.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace PiRoverController.UWP
{
    class PlatformToast : IPlatformToast
    {
        public void ShowToast(string toastMessage)
        {
            ShowMessage(toastMessage, 2.0);
        }

        private void ShowMessage(string message, double duration)
        {
            FlyoutPlacementMode position = FlyoutPlacementMode.Bottom;

            var label = new TextBlock
            {
                Text = message,
                Foreground = new SolidColorBrush(Windows.UI.Colors.White),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            var style = new Windows.UI.Xaml.Style { TargetType = typeof(FlyoutPresenter) };
            style.Setters.Add(new Windows.UI.Xaml.Setter(Control.BackgroundProperty, new SolidColorBrush(Windows.UI.Colors.Gray)));
            style.Setters.Add(new Windows.UI.Xaml.Setter(FrameworkElement.MaxHeightProperty, 1));
            var flyout = new Flyout
            {
                Content = label,
                AllowFocusOnInteraction = false,
                Placement = position,
                FlyoutPresenterStyle = style,
            };

            flyout.OverlayInputPassThroughElement = Window.Current.Content;
            FrameworkElement mainElement = Window.Current.Content as FrameworkElement;
            flyout.ShowAt(mainElement);

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(duration) };
            timer.Tick += (sender, e) => {
                timer.Stop();
                flyout.Hide();
            };
            timer.Start();
        }
    }
}
