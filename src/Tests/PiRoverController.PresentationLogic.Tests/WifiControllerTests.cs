using Moq;
using NUnit.Framework;
using PiRoverController.Common;
using PiRoverController.Common.Enums;
using PiRoverController.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PiRoverController.PresentationLogic.Tests
{
    [TestFixture]
    [Category("unittests")]
    public class WifiControllerTests : BaseTests
    {
        [Test]
        public void GPIOInited_When_PingSuccess()
        {
            var expectedUri = new Uri("http://192.168.0.22:8080/RoverControls/initGPIO");
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(Task.FromResult<bool>(true));
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var sut = CreateViewModel(); 

           sut.OnAppearingCommand.Execute(null);

            httpClientMock.Verify(x => x.GetAsync(expectedUri), Times.Once);
        }

        [Test]
        public void GPIONotInited_When_PingSuccess()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(Task.FromResult<bool>(false));
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var sut = CreateViewModel();

            sut.OnAppearingCommand.Execute(null);


            httpClientMock.Verify(x => x.GetAsync(It.IsAny<Uri>()), Times.Never);
        }

        [Test]
        public void RetrieveSettings()
        {
            var mockSettings = TestHelper.GetMockSettingsData().ToList();
            settingAccessMock.Setup(x => x.GetSettings()).Returns(mockSettings);
            var sut = CreateViewModel();

            sut.OnAppearingCommand.Execute(null);


            for (int i = 0; i < sut.Settings.Count; i++)
            {
                Assert.That(sut.Settings.ElementAt(i).Id, Is.EqualTo(mockSettings[i].Id));
                Assert.That(sut.Settings.ElementAt(i).SettingName, Is.EqualTo(mockSettings[i].SettingName));
                Assert.That(sut.Settings.ElementAt(i).SettingType, Is.EqualTo(mockSettings[i].SettingType));
                Assert.That(sut.Settings.ElementAt(i).SettingValue, Is.EqualTo(mockSettings[i].SettingValue));
            }
        }

        [Test]
        public void DriveRover_WhileAttemptingToConnect()
        {
            var mockSettings = TestHelper.GetMockSettingsData().ToList();
            settingAccessMock.Setup(x => x.GetSettings()).Returns(mockSettings);

            var sut = CreateViewModel();
            sut.OnAppearingCommand.Execute(null);
            sut.RoverConnection = RoverConnection.Trying_To_Connect;

            sut.GoForwardsCommand.Execute(null);
            sut.GoBackwardsCommand.Execute(null);
            sut.GoLeftCommand.Execute(null);
            sut.GoLeftCommand.Execute(null);
            sut.GoRightCommand.Execute(null);
            sut.GoRightCommand.Execute(null);
            sut.StopForwardsAndBackwardCommand.Execute(null);

            PopUpsMock.Verify(x => x.ShowToast("Cannot Drive - Trying to Connect to Rover"), Times.Exactly(7));
            httpClientMock.Verify(x => x.GetAsync(It.IsAny<Uri>()), Times.Never);
        }

        [Test]
        public void GoForwardTest_Valid()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(Task.FromResult<bool>(true));
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var expectedUri = new Uri("http://192.168.0.22:8080/RoverControls/forward");
            var sut = CreateViewModel();
            sut.OnAppearingCommand.Execute(null);

            sut.GoForwardsCommand.Execute(null);

            httpClientMock.Verify(x => x.GetAsync(expectedUri), Times.Once);
        }

        [Test]
        public void GoBackTest_Valid()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(Task.FromResult<bool>(true));
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var expectedUri = new Uri("http://192.168.0.22:8080/RoverControls/backward");
            var sut = CreateViewModel();
            sut.OnAppearingCommand.Execute(null);

            sut.GoBackwardsCommand.Execute(null);

            httpClientMock.Verify(x => x.GetAsync(expectedUri), Times.Once);
        }

        [Test]
        public void GoLeftTest_Valid()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(Task.FromResult<bool>(true));
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var expectedUri = new Uri("http://192.168.0.22:8080/RoverControls/left");
            var sut = CreateViewModel();
            sut.OnAppearingCommand.Execute(null);

            sut.GoLeftCommand.Execute(null);

            httpClientMock.Verify(x => x.GetAsync(expectedUri), Times.Once);
        }

        [Test]
        public void GoRightTest_Valid()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(Task.FromResult<bool>(true));
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var expectedUri = new Uri("http://192.168.0.22:8080/RoverControls/right");
            var sut = CreateViewModel();
            sut.OnAppearingCommand.Execute(null);

            sut.GoRightCommand.Execute(null);

            httpClientMock.Verify(x => x.GetAsync(expectedUri), Times.Once);
        }

        [Test]
        public void StopForwardAndBackward_Valid()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(Task.FromResult<bool>(true));
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var expectedUri = new Uri("http://192.168.0.22:8080/RoverControls/stopForwardAndBackward");
            var sut = CreateViewModel();
            sut.OnAppearingCommand.Execute(null);

            sut.StopForwardsAndBackwardCommand.Execute(null);

            httpClientMock.Verify(x => x.GetAsync(expectedUri), Times.Once);
        }

        [Test]
        public void StopLeftAndRight_Valid()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(Task.FromResult<bool>(true));
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var expectedUri = new Uri("http://192.168.0.22:8080/RoverControls/stopLeftAndRight");
            var sut = CreateViewModel();
            sut.OnAppearingCommand.Execute(null);

            sut.StopLeftAndRightCommand.Execute(null);

            httpClientMock.Verify(x => x.GetAsync(expectedUri), Times.Once);
        }

        [Test]
        public void CheckRoverConnection_ConnectionAvailable()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(Task.FromResult<bool>(true));
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var sut = CreateViewModel();
            sut.OnAppearingCommand.Execute(null);

            sut.CheckRoverConnectionCommand.Execute(null);

            Assert.That(sut.RoverConnection, Is.EqualTo(RoverConnection.Rover_Detected));
        }

        [Test]
        public void CheckRoverConnection_ConnectionUnavailable()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(Task.FromResult<bool>(false));
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetBadMockSettingsData());
            var sut = CreateViewModel();
            sut.OnAppearingCommand.Execute(null);

            sut.CheckRoverConnectionCommand.Execute(null);

            Assert.That(sut.RoverConnection, Is.EqualTo(RoverConnection.Not_Detected));
        }

        [Test]
        public void InitGPIOs_WithBadUri()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(Task.FromResult(true)); //should never return true from bad uri, but best testing the case.
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetBadMockSettingsData());
            var sut = CreateViewModel();
            sut.OnAppearingCommand.Execute(null);

            PopUpsMock.Verify(x => x.ShowToast("Base URL httqwqdp://192.168.0._43422:80d80/ is an incorrect URL Format"), Times.Once);
        }

        [Test]
        public void DriveRover_WithBadUri_AndbadBaseURI()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(Task.FromResult(true)); //should never return true from bad uri, but best testing the case.
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetBadMockSettingsData());
            var sut = CreateViewModel();
            sut.OnAppearingCommand.Execute(null);

            sut.GoForwardsCommand.Execute(null);
            PopUpsMock.Verify(x => x.ShowToast("No base URL Found...is it in the Correct Format?"), Times.Once);
        }

        [Test]
        public void DriveRover_WithBadUri_AndGoodBaseURI()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(Task.FromResult(true)); //should never return true from bad uri, but best testing the case.
            var badSettings = TestHelper.GetBadMockSettingsData().ToList();
            badSettings[0].SettingValue = "http://192.168.0.22:8080/";

            settingAccessMock.Setup(x => x.GetSettings()).Returns(badSettings);
            var sut = CreateViewModel();
            sut.OnAppearingCommand.Execute(null);

            sut.GoForwardsCommand.Execute(null);
            //PopUpsMock.Verify(x => x.ShowToast("No base URL Found...is it in the Correct Format?"), Times.Once);
        }

        private WifiControllerViewModel CreateViewModel()
        {
            var vm =  new WifiControllerViewModel(commandGeneratorMock.Object,
                                               settingAccessMock.Object,
                                               navigatorMock.Object,
                                               httpClientMock.Object,
                                               PopUpsMock.Object);

            return vm;
        }
    }
}
