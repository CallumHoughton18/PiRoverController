using PiRoverController.Common.Interfaces;
using PiRoverController.PresentationLogic;
using PiRoverController.PresentationLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PiRoverController.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WifiControlView : ContentPage
    {
        public WifiControlView()
        {
            InitializeComponent();
            //SURPRISE SURPRISE a shitty workaround because UWP support is flaky at best in Xamarin. Visual states do not work as expected.
            //https://github.com/xamarin/Xamarin.Forms/issues/4910
            if (Device.RuntimePlatform == Device.UWP)
            {
                var imageButtons = new ImageButton[] { ForwardArrow, BackwardArrow, LeftArrow, RightArrow };
                foreach (var imageButton in imageButtons) VisualStateManager.SetVisualStateGroups(imageButton, new VisualStateGroupList());
            }
        }

        private void Animate_Sync_Button(object sender, EventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                //can be called multiple times through monkey testing, but Xamarin animation handles it without a performance hit or crashing.
                //if more animation needed it'd be best to create a central animation class to handle this, rather than it being in code behind view.
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await button.RotateTo(360, 500);
                    await button.RotateTo(0, 0);
                });
            }
        }
    }
}