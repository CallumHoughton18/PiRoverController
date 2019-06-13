using Ninject;
using PiRoverController.Common.Interfaces;
using PiRoverController.Factories;
using PiRoverController.Implementations;
using PiRoverController.PresentationLogic;
using PiRoverController.PresentationLogic.Interfaces;
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
        private IKernel _container;

        public App()
        {
            InitializeComponent();
            var settings = new NinjectSettings() { LoadExtensions = false };
            _container = new StandardKernel(settings);
            ConfigureContainer();
            ComposeObjects();
        }

        private void ConfigureContainer()
        {
            string databasePath = "";
            if (Device.RuntimePlatform == Device.UWP) databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db");
            else databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db");

            _container.Bind<ICommandGenerator>().To<CommandGenerator>()
                .InSingletonScope();

            _container.Bind<ISettingAccess>().To<SettingReaderWriterSQL>()
                .WithConstructorArgument<string>(databasePath);

            _container.Bind<IHTTPClient>().To<HTTPClientService>();

            _container.Bind<IViewFactory>().To<ViewFactory>()
                .InSingletonScope()
                .WithConstructorArgument(_container);

            _container.Bind<INavigator>().To<Navigator>()
                .InSingletonScope()
                .WithConstructorArgument(_container.Get<IViewFactory>());

            _container.Get<IViewFactory>().Register<SettingsViewModel, SettingsView>();
            _container.Get<IViewFactory>().Register<WifiControllerViewModel, WifiControlView>();
        }

        private void ComposeObjects()
        {
            MainPage = new NavigationPage(_container.Get<IViewFactory>().Resolve<WifiControllerViewModel>());

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
