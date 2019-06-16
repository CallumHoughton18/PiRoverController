using PiRoverController.PresentationLogic;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PiRoverController.Factories
{
    interface IViewFactory
    {
        Page Resolve<ViewModel>() where ViewModel : BaseViewModel;
        void Register<ViewModel, View>() where ViewModel : BaseViewModel where View : Page;

        void RegisterAndCache<ViewModel, View>() where ViewModel : BaseViewModel where View : Page;
    }
}
