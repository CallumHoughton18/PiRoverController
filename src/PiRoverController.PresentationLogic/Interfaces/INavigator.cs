using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PiRoverController.PresentationLogic.Interfaces
{
    public interface INavigator
    {
        Task PopAsync();

        Task PopModalAsync();

        Task PopToRootAsync();

        Task PushAsync<ViewModel>() where ViewModel : BaseViewModel;

        Task PushModalAsync<ViewModel>() where ViewModel : BaseViewModel;
    }
}
