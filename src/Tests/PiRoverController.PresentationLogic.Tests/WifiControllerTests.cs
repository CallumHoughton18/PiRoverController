using Moq;
using NUnit.Framework;
using PiRoverController.Common;
using PiRoverController.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiRoverController.PresentationLogic.Tests
{
    public class WifiControllerTests : BaseTests
    {
        [Test]
        public async Task GPIOInited_When_PingSuccess()
        {
            var expectedUri = new Uri("http://192.168.0.22:8080/RoverControls/initGPIO");
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(true);
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var sut = CreateViewModel();

           await sut.LoadData();

            httpClientMock.Verify(x => x.GetAsync(expectedUri), Times.Once);
        }

        [Test]
        public async Task GPIONotInited_When_PingSuccess()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(false);
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var sut = CreateViewModel();

            await sut.LoadData();

            httpClientMock.Verify(x => x.GetAsync(It.IsAny<Uri>()), Times.Never);
        }

        [Test]
        public async Task RetrieveSettings()
        {
            var mockSettings = TestHelper.GetMockSettingsData().ToList();
            settingAccessMock.Setup(x => x.GetSettings()).Returns(mockSettings);
            var sut = CreateViewModel();

            await sut.LoadData();

            for (int i = 0; i < sut.Settings.Count; i++)
            {
                Assert.That(sut.Settings.ElementAt(i).Id, Is.EqualTo(mockSettings[i].Id));
                Assert.That(sut.Settings.ElementAt(i).SettingName, Is.EqualTo(mockSettings[i].SettingName));
                Assert.That(sut.Settings.ElementAt(i).SettingType, Is.EqualTo(mockSettings[i].SettingType));
                Assert.That(sut.Settings.ElementAt(i).SettingValue, Is.EqualTo(mockSettings[i].SettingValue));
            }
        }

        [Test]
        public async Task GoForwardTest_Valid()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(true);
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var expectedUri = new Uri("http://192.168.0.22:8080/RoverControls/forward");
            var sut = CreateViewModel();
            await sut.LoadData();

            sut.GoForwardsCommand.Execute(null);

            httpClientMock.Verify(x => x.GetAsync(expectedUri), Times.Once);
        }

        [Test]
        public async Task GoBackTest_Valid()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(true);
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var expectedUri = new Uri("http://192.168.0.22:8080/RoverControls/backward");
            var sut = CreateViewModel();
            await sut.LoadData();

            sut.GoBackwardsCommand.Execute(null);

            httpClientMock.Verify(x => x.GetAsync(expectedUri), Times.Once);
        }

        [Test]
        public async Task GoLeftTest_Valid()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(true);
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var expectedUri = new Uri("http://192.168.0.22:8080/RoverControls/left");
            var sut = CreateViewModel();
            await sut.LoadData();

            sut.GoLeftCommand.Execute(null);

            httpClientMock.Verify(x => x.GetAsync(expectedUri), Times.Once);
        }

        [Test]
        public async Task GoRightTest_Valid()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(true);
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var expectedUri = new Uri("http://192.168.0.22:8080/RoverControls/right");
            var sut = CreateViewModel();
            await sut.LoadData();

            sut.GoRightCommand.Execute(null);

            httpClientMock.Verify(x => x.GetAsync(expectedUri), Times.Once);
        }

        [Test]
        public async Task StopForwardAndBackward_Valid()
        {
            httpClientMock.Setup(x => x.HostAvailable(It.IsAny<Uri>())).Returns(true);
            settingAccessMock.Setup(x => x.GetSettings()).Returns(TestHelper.GetMockSettingsData());
            var expectedUri = new Uri("http://192.168.0.22:8080/RoverControls/stopForwardAndBackward");
            var sut = CreateViewModel();
            await sut.LoadData();

            sut.StopForwardsAndBackwardCommand.Execute(null);

            httpClientMock.Verify(x => x.GetAsync(expectedUri), Times.Once);
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
