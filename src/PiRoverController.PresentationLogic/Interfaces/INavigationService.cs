using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PiRoverController.PresentationLogic.Interfaces
{
    public interface INavigationService
    { 
        Task PushModalSettingsPage();
        Task PopModalAsync();
    }
}
