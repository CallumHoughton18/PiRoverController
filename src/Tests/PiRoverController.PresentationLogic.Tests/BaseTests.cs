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
        protected Mock<IPopUps> PopUpsMock = new Mock<IPopUps>();

        [SetUp]
        public void ResetMocks()
        {
            commandGeneratorMock = new Mock<ICommandGenerator>();
            commandGeneratorMock.Setup(x => x.GenerateCommand(It.IsAny<Action>())).Returns((Action x) => { return new Command(x); });
            settingAccessMock = new Mock<ISettingAccess>();
            navigatorMock = new Mock<INavigator>();
            httpClientMock = new Mock<IHTTPClient>();
            PopUpsMock = new Mock<IPopUps>();
        }


    }
}
