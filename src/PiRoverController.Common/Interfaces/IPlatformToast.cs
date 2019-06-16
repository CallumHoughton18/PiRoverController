using System;
using System.Collections.Generic;
using System.Text;

namespace PiRoverController.Common.Interfaces
{
    public interface IPlatformToast
    {
        void ShowToast(string toastMessage);
    }
}
