using PiRoverController.Common.Interfaces;
using PiRoverController.Common.Models;
using PiRoverController.PresentationLogic.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PiRoverController.PresentationLogic
{
    public class WifiControllerViewModel : BaseViewModel
    {

        private readonly ICommandGenerator _commandGenerator;
        private readonly ISettingAccess _settingAccess;
        private readonly IHTTPClient _httpClient;

        private Uri _baseUri;
        List<Setting> _settings;
        public ICommand GoForwardsCommand { get; private set; }
        public ICommand GoBackwardsCommand { get; private set; }
        public ICommand GoLeftCommand { get; private set; }
        public ICommand GoRightCommand { get; private set; }
        public ICommand StopForwardsAndBackwardCommand { get; private set; }
        public ICommand GoToSettingsCommand { get; private set; }
        
        public ICommand OnAppearingCommand { get; private set; }

        public WifiControllerViewModel(ICommandGenerator commandGenerator, ISettingAccess settingAccess, INavigator navigator, IHTTPClient httpClient): base(navigator)
        {
            _commandGenerator = commandGenerator;
            _settingAccess = settingAccess;
            _httpClient = httpClient;

            OnAppearingCommand = _commandGenerator.GenerateCommand(async () => await LoadData());

            GoForwardsCommand = _commandGenerator.GenerateCommand(async () => await GoForward());
            GoBackwardsCommand = _commandGenerator.GenerateCommand(async () => await GoBackward());
            GoLeftCommand = _commandGenerator.GenerateCommand(async () => await GoLeft() /*TODO:if already left, reset */);
            GoRightCommand = _commandGenerator.GenerateCommand(async () => await GoRight() /*TODO:if already right, reset */);
            StopForwardsAndBackwardCommand = _commandGenerator.GenerateCommand(async () => await StopForwardsAndBackwards());
            GoToSettingsCommand = _commandGenerator.GenerateCommand(async () => await GoToSettings());
        }

        private async Task GoToSettings()
        {
            await _navigator.PushModalAsync<SettingsViewModel>();
        }

        public async Task LoadData()
        {
            //TODO:if connection available...init GPIO.
            //check for better way of finding settings maybe?
            //Implement reset direction(left right) on web server.
            //Implement clean shutting down of GPIOs?
            _settings = new List<Setting>(_settingAccess.GetSettings());
            _baseUri = new Uri(_settings.Where(x => x.Id == 1).FirstOrDefault().SettingValue);

            if (_httpClient.HostAvailable(_baseUri))
                await InitGPIOs();
            //await InitGPIOs(); //freezes if can't call to server?
        }

        private async Task InitGPIOs()
        {
            Uri forwardUri = new Uri(_baseUri, _settings.Where(x => x.Id == 2).FirstOrDefault().SettingValue);
            await _httpClient.GetAsync(forwardUri);
        }

        private async Task GoForward()
        {
            Uri forwardUri = new Uri(_baseUri, _settings.Where(x => x.Id == 3).FirstOrDefault().SettingValue);
            await _httpClient.GetAsync(forwardUri);
        }

        private async Task GoBackward()
        {
            Uri forwardUri = new Uri(_baseUri, _settings.Where(x => x.Id == 4).FirstOrDefault().SettingValue);
            await _httpClient.GetAsync(forwardUri);
        }

        private async Task GoLeft()
        {
            Uri forwardUri = new Uri(_baseUri, _settings.Where(x => x.Id == 5).FirstOrDefault().SettingValue);
            await _httpClient.GetAsync(forwardUri);
        }

        private async Task GoRight()
        {
            Uri forwardUri = new Uri(_baseUri, _settings.Where(x => x.Id == 6).FirstOrDefault().SettingValue);
            await _httpClient.GetAsync(forwardUri);
        }

        private async Task StopForwardsAndBackwards()
        {
            Uri forwardUri = new Uri(_baseUri, _settings.Where(x => x.Id == 8).FirstOrDefault().SettingValue);
            await _httpClient.GetAsync(forwardUri);
        }
    }
}
