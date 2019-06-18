using Moq;
using NUnit.Framework;
using PiRoverController.Common.Interfaces;
using PiRoverController.PresentationLogic.Interfaces;
using System;
using Xamarin.Forms;

namespace PiRoverController.PresentationLogic.Tests
{
    public abstract class BaseTests
    {
        protected Mock<ICommandGenerator> commandGeneratorMock = new Mock<ICommandGenerator>();
        protected Mock<ISettingAccess> settingAccessMock = new Mock<ISettingAccess>();
        protected Mock<INavigator> navigatorMock = new Mock<INavigator>();
        protected Mock<IHTTPClient> httpClientMock = new Mock<IHTTPClient>();
        protected Mock<IPlatformToast> PopUpsMock = new Mock<IPlatformToast>();

        [SetUp]
        public void ResetMocks()
        {
            //should probably implement a stub CommandGenerator, rather than relying on Xamarin Forms implementation.

            commandGeneratorMock = new Mock<ICommandGenerator>();
            commandGeneratorMock.Setup(x => x.GenerateCommand(It.IsAny<Action>())).Returns((Action x) => { return new Command(x); });
            commandGeneratorMock.Setup(x => x.GenerateCommand(It.IsAny<Action>(), It.IsAny<Func<bool>>())).Returns((Action x, Func<bool> func) => { return new Command(x, func); });
            settingAccessMock = new Mock<ISettingAccess>();
            navigatorMock = new Mock<INavigator>();
            httpClientMock = new Mock<IHTTPClient>();
            PopUpsMock = new Mock<IPlatformToast>();
        }


    }
}
