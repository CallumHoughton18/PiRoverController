using Ninject;
using PiRoverController.PresentationLogic;
using PiRoverController.PresentationLogic.Interfaces;
using PiRoverController.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PiRoverController.Implementations
{
    class NavigationService : INavigationService
    {
        IKernel _rootContainer;

        public NavigationService(IKernel rootContainer)
        {
            _rootContainer = rootContainer;
        }

        public async Task PushModalSettingsPage()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(_rootContainer.Get<SettingsView>());
        }

        public async Task PopModalAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
