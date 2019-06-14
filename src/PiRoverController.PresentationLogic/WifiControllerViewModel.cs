using PiRoverController.Common.Enums;
using PiRoverController.Common.Interfaces;
using PiRoverController.Common.Models;
using PiRoverController.PresentationLogic.Interfaces;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
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
        private readonly IPopUps _popUps;
        private readonly object _roverDirectionLock = new object();

        private RoverDirection _currentRoverDirection = RoverDirection.None; //rover should start off motionless.
        private bool _initLoad = true;

        private Uri _baseUri;
        public Uri BaseUri
        {
            get
            {
                return _baseUri;
            }
        }

        BlockingCollection<Setting> _settings = new BlockingCollection<Setting>();
        public BlockingCollection<Setting> Settings
        {
            get
            {
                return _settings;
            }
        }

        private bool _loadingData = true;
        public bool LoadingData
        {
            get
            {
                return _loadingData;
            }
            set
            {
                if (_loadingData != value)
                {
                    _loadingData = value;
                    OnPropertyChanged(nameof(LoadingData));
                }
            }
        }

        private string _loadingMessage = "";
        public string LoadingMessage
        {
            get
            {
                return _loadingMessage;
            }
            set
            {
                if (_loadingMessage != value)
                {
                    _loadingMessage = value;
                    OnPropertyChanged(nameof(LoadingMessage));
                }
            }
        }

        public ICommand GoForwardsCommand { get; private set; }
        public ICommand GoBackwardsCommand { get; private set; }
        public ICommand GoLeftCommand { get; private set; }
        public ICommand GoRightCommand { get; private set; }
        public ICommand StopForwardsAndBackwardCommand { get; private set; }
        public ICommand GoToSettingsCommand { get; private set; }
        public ICommand OnAppearingCommand { get; private set; }

        public WifiControllerViewModel(ICommandGenerator commandGenerator, ISettingAccess settingAccess, INavigator navigator, IHTTPClient httpClient,
                                       IPopUps popUps) : base(navigator)
        {
            _commandGenerator = commandGenerator;
            _settingAccess = settingAccess;
            _httpClient = httpClient;
            _popUps = popUps;

            OnAppearingCommand = _commandGenerator.GenerateCommand(async () => await LoadData());
            GoForwardsCommand = _commandGenerator.GenerateCommand(async () => await DriveRover(RoverDriverInstructions.GoForwards));
            GoBackwardsCommand = _commandGenerator.GenerateCommand(async () => await DriveRover(RoverDriverInstructions.GoBackwards));

            GoLeftCommand = _commandGenerator.GenerateCommand(async () =>
            {
                await SetRoverDirection(RoverDriverInstructions.GoLeft, RoverDirection.Left);
            });
            GoRightCommand = _commandGenerator.GenerateCommand(async () =>
            {
                await SetRoverDirection(RoverDriverInstructions.GoRight, RoverDirection.Right);
            });
            StopForwardsAndBackwardCommand = _commandGenerator.GenerateCommand(async () => await DriveRover(RoverDriverInstructions.StopFowardsAndBackwards));
            GoToSettingsCommand = _commandGenerator.GenerateCommand(async () => await GoToSettings());
        }

        private async Task GoToSettings()
        {
            await _navigator.PushModalAsync<SettingsViewModel>();
        }

        public async Task LoadData()
        {
            LoadingData = true;
            LoadingMessage = "Loading Setting...";
            //Implement clean shutting down of GPIOs?
            //Add finished binding message to display when loading data done.
            //add the resetdirection to go left/right if already on that direction.
            //Block user input on settingsvm until settings loaded, maybe have as singleton for quicker access?
            //more unit tests.
            await Task.Delay(4000);

            _settings.GetConsumingEnumerable();
            _settings = new BlockingCollection<Setting>();

            foreach (var setting in _settingAccess.GetSettings())
            {
                _settings.Add(setting);
            }

            _settings.CompleteAdding();

            //change this
            var baseURLSetting = GetSettingByID((int)SettingsIDs.BaseURL);

            if (baseURLSetting == null) throw new NullReferenceException("Base URL Setting not found...app will not be functional");

            _baseUri = new Uri(baseURLSetting.SettingValue);


            if (_initLoad && _httpClient.HostAvailable(_baseUri))
            {
                await InitGPIOs();
                _initLoad = false;
            }

            LoadingMessage = "Done!";
            LoadingData = false;;

            //await Task.Delay(3000);
            //LoadingData = true;
        }

        private async Task DriveRover(RoverDriverInstructions roverInstruction)
        {
            if (_settings.IsAddingCompleted && _baseUri != null)
            {
                try
                {
                    Setting requiredSetting = GetSettingByID((int)roverInstruction);
                    if (requiredSetting != null)
                    {
                        Uri instructionUri = new Uri(_baseUri, requiredSetting.SettingValue);
                        await _httpClient.GetAsync(instructionUri);
                    }
                }
                catch (WebException)
                {

                }
            }

            else
            {
                _popUps.ShowToast("Still loading settings...");
            }
        }

        private async Task SetRoverDirection(RoverDriverInstructions roverInstruction, RoverDirection requestedDirection)
        {
            if (_currentRoverDirection != requestedDirection)
            {
                await DriveRover(roverInstruction);
                lock (_roverDirectionLock)
                {
                    _currentRoverDirection = requestedDirection;
                }
            }
            else
            {
                await DriveRover(RoverDriverInstructions.StopLeftAndRight);
                lock (_roverDirectionLock)
                {
                    _currentRoverDirection = RoverDirection.None;
                }
            }
        }

        private async Task InitGPIOs()
        {
            if (_settings.IsAddingCompleted && _baseUri != null)
            {
                Setting requiredSetting = GetSettingByID((int)SettingsIDs.InitGPIOs);
                if (requiredSetting != null)
                {
                    Uri initUri = new Uri(_baseUri, requiredSetting.SettingValue);
                    await _httpClient.GetAsync(initUri);
                }
            }

            else
            {
                //display still loading message
            }
        }

        private Setting GetSettingByID(int ID)
        {
            return _settings.Where(x => x.Id == ID).FirstOrDefault();
        }
    }
}
