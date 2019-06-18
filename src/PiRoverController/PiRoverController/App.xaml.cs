using Ninject;
using PiRoverController.Common.Interfaces;
using PiRoverController.Factories;
using PiRoverController.Implementations;
using PiRoverController.Interfaces;
using PiRoverController.PresentationLogic;
using PiRoverController.PresentationLogic.Interfaces;
using PiRoverController.SettingAccessSQL;
using PiRoverController.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;
using Xamarin.Forms.Xaml;

namespace PiRoverController
{
    public partial class App : Xamarin.Forms.Application
    {
        public IKernel Container { get; private set; }

        public App(IPlatformToast platformToast):this(platformToast, new ServerConnection())
        {
        }

        public App(IPlatformToast platformToast, IPingConnection pingConnection)
        {
            InitializeComponent();
            SetPlatformConfig();
            var settings = new NinjectSettings() { LoadExtensions = false };
            Container = new StandardKernel(settings);
            ConfigureContainer(platformToast, pingConnection);
            ComposeObjects();
        }

        private void ConfigureContainer(IPlatformToast platformToast, IPingConnection pingConnection)
        {
            string databasePath = "";
            if (Device.RuntimePlatform == Device.UWP) databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db");
            else databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db");

            Container.Bind<IPlatformToast>().ToConstant(platformToast);

            Container.Bind<IPingConnection>().ToConstant(pingConnection);

            Container.Bind<ICommandGenerator>().To<CommandGenerator>()
                .InSingletonScope();

            Container.Bind<ISettingAccess>().To<SettingReaderWriterSQL>()
                .WithConstructorArgument<string>(databasePath);

            Container.Bind<IHTTPClient>().To<HTTPClientService>().WithConstructorArgument("pingConnection", Container.Get<IPingConnection>());

            Container.Bind<IViewFactory>().To<ViewFactory>()
                .InSingletonScope()
                .WithConstructorArgument(Container);

            Container.Bind<INavigator>().To<Navigator>()
                .InSingletonScope()
                .WithConstructorArgument(Container.Get<IViewFactory>());

            Container.Get<IViewFactory>().Register<WifiControllerViewModel, WifiControlView>();
            Container.Get<IViewFactory>().RegisterAndCache<SettingsViewModel, SettingsView>();
        }

        private void ComposeObjects()
        {
            MainPage = new Xamarin.Forms.NavigationPage(Container.Get<IViewFactory>().Resolve<WifiControllerViewModel>());
        }

        public void SetPlatformConfig()
        {
            this.On<Xamarin.Forms.PlatformConfiguration.Android>().SendAppearingEventOnResume(false);
            this.On<Xamarin.Forms.PlatformConfiguration.Android>().SendDisappearingEventOnPause(false);
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
