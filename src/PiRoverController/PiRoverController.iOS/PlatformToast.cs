using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PiRoverController.Common.Interfaces;
using UIKit;

namespace PiRoverController.iOS
{
    class PlatformToast : IPlatformToast
    {
        NSTimer alertDelay;
        UIAlertController alert;


        public void ShowToast(string toastMessage)
        {
            ShowAlert(toastMessage, .0);
        }

        private void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                dismissMessage();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        private void dismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}