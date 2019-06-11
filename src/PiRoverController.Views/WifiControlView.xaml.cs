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
        public WifiControlView(ICommandGenerator commandGenerator, ISettingAccess settingAccess, INavigationService navigationService)
        {
            InitializeComponent();
            Task.Run(async () =>
            {
                var vm = await WifiControllerViewModel.Initialize(commandGenerator, settingAccess, navigationService);
                Device.BeginInvokeOnMainThread(() => BindingContext = vm);
            });
        }
    }
}