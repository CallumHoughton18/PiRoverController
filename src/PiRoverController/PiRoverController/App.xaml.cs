using Ninject;
using PiRoverController.Common.Interfaces;
using PiRoverController.Implementations;
using PiRoverController.SettingAccessSQL;
using PiRoverController.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PiRoverController
{
    public partial class App : Application
    {
        IKernel container;

        public App()
        {
            InitializeComponent();
            var settings = new Ninject.NinjectSettings() { LoadExtensions = false };
            container = new Ninject.StandardKernel(settings);
            ConfigureContainer();
            ComposeObjects();
        }

        private void ConfigureContainer()
        {
            string databasePath = "";
            if (Device.RuntimePlatform == Device.UWP) databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db");
            else databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db");

            container.Bind<ICommandGenerator>().To<CommandGenerator>()
                .InSingletonScope();

            container.Bind<ISettingAccess>().To<SettingReaderWriterSQL>()
                .WithConstructorArgument<string>(databasePath);
        }

        private void ComposeObjects()
        {
            MainPage = new NavigationPage(container.Get<WifiControlView>());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
