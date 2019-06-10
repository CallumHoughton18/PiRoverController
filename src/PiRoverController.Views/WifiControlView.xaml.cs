﻿using PiRoverController.Common.Interfaces;
using PiRoverController.PresentationLogic;
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
        public WifiControlView(ICommandGenerator commandGenerator, ISettingAccess settingAccess)
        {
            InitializeComponent();
            Task.Run(async () =>
            {
                var vm = await WifiControllerViewModel.Initialize(commandGenerator, settingAccess);
                Device.BeginInvokeOnMainThread(() => BindingContext = vm);
            });
        }
    }
}