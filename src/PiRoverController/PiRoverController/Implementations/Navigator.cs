
using PiRoverController.Factories;
using PiRoverController.PresentationLogic;
using PiRoverController.PresentationLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PiRoverController.Implementations
{
    sealed class Navigator : INavigator
    {
        IViewFactory _viewFactory;
        public Navigator(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }
        public async Task PopAsync()
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task PopModalAsync()
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public async Task PopToRootAsync()
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        public async Task PushAsync<ViewModel>() where ViewModel : BaseViewModel
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(_viewFactory.Resolve<ViewModel>());
        }

        public async Task PushModalAsync<ViewModel>() where ViewModel : BaseViewModel
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(_viewFactory.Resolve<ViewModel>());
        }
    }
}
