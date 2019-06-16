using Moq;
using NUnit.Framework;
using PiRoverController.Common.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PiRoverController.PresentationLogic.Tests
{
    public class SettingsViewModelTests : BaseTests
    {
        [Test]
        public async Task InitialLoadTest_Valid()
        {
            var expectedSettings = TestHelper.GetMockSettingsData();
            settingAccessMock.Setup(x => x.GetSettings()).Returns(expectedSettings);

            var sut = SetupVM();

            Assert.That(sut.Settings, Is.Null);

            sut.InitialLoad();

            await Task.Delay(50); //mimic loading data time as InitialLoad() executes a task on another threads

            Assert.That(sut.Settings, Is.EqualTo(expectedSettings));
        }

        [Test]
        public void SaveSettings_Valid()
        {
            var settings = TestHelper.GetMockSettingsData();

            var sut = SetupVM();
            sut.Settings = new ObservableCollection<Setting>(settings);

            sut.SaveSettingsCommand.Execute(null);

            foreach (var setting in sut.Settings)
            {
                settingAccessMock.Verify(x => x.SaveSetting(setting), Times.Once);
            }
            navigatorMock.Verify(x => x.PopModalAsync(), Times.Once);
        }

        [Test]
        public void SaveSettings_NullSettings()
        {
            var sut = SetupVM();

            sut.SaveSettingsCommand.Execute(null);

            PopUpsMock.Verify(x => x.ShowToast("Could not save settings...try again"));
        }
        private SettingsViewModel SetupVM()
        {
            var vm = new SettingsViewModel(settingAccessMock.Object, commandGeneratorMock.Object, navigatorMock.Object, PopUpsMock.Object);
            return vm;
        }
    }
}
