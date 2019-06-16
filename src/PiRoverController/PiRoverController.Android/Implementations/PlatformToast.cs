using Android.App;
using Android.Widget;
using PiRoverController.Common.Interfaces;

namespace PiRoverController.Droid.Implementations
{
    class PlatformToast : IPlatformToast
    {
        public void ShowToast(string toastMessage)
        {
            Toast.MakeText(Application.Context, toastMessage, ToastLength.Long).Show();
        }
    }
}