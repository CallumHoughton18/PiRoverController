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
        private readonly IPlatformToast _platformToast;
        private readonly object _roverDirectionLock = new object();

        private RoverDirection _currentRoverDirection = RoverDirection.None; //rover should start off motionless.
        private bool _initLoad = true;

        private RoverConnection _roverConnection = RoverConnection.Not_Detected;
        public RoverConnection RoverConnection
        {
            get
            {
                return _roverConnection;
            }
            set
            {
                if (_roverConnection != value)
                {
                    _roverConnection = value;
                    OnPropertyChanged(nameof(RoverConnection));
                }
            }
        }

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
        public ICommand CheckRoverConnectionCommand { get; private set; }
        public ICommand GoToSettingsCommand { get; private set; }

        public WifiControllerViewModel(ICommandGenerator commandGenerator, ISettingAccess settingAccess, INavigator navigator, IHTTPClient httpClient,
                                       IPlatformToast platformToast) : base(navigator)
        {
            _commandGenerator = commandGenerator;
            _settingAccess = settingAccess;
            _httpClient = httpClient;
            _platformToast = platformToast;

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            OnAppearingCommand = _commandGenerator.GenerateCommand(async () =>
            {
                //possible here, if on appearing fails, display alert about incorrect settings configuration THEN crash the app. 
                //or just add global unhandled exception handler to each platform.

                OnAppearingLoad();
                if (_initLoad)
                {
                    await InitializeRover();
                    _initLoad = false;
                }
            });

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
            CheckRoverConnectionCommand = _commandGenerator.GenerateCommand(async () => await SetRoverConnectionStatus());
        }

        public override void InitialLoad()
        {
        }
        private async Task GoToSettings()
        {
            await _navigator.PushModalAsync<SettingsViewModel>();
        }

        private void OnAppearingLoad()
        {
            SetLoadingMessage("Loading Settings...", true);
            //Add finished binding message to display when loading data done.
            //more unit tests.

            _settings.GetConsumingEnumerable();
            _settings = new BlockingCollection<Setting>();

            foreach (var setting in _settingAccess.GetSettings())
            {
                _settings.Add(setting);
            }

            _settings.CompleteAdding();

            var baseURLSetting = GetSettingByID((int)SettingsIDs.BaseURL);

            if (baseURLSetting == null) throw new NullReferenceException("Base URL Setting not found...app will not be functional");

            _baseUri = new Uri(baseURLSetting.SettingValue);

            SetLoadingMessage("Done!", false);
        }

        private async Task InitializeRover()
        {
            RoverConnection = RoverConnection.Trying_To_Connect;
            SetLoadingMessage("Setting Up Rover...", true);

            var hostAvailable = await _httpClient.HostAvailable(_baseUri);
            if (hostAvailable)
            {
                await InitGPIOs();
                RoverConnection = RoverConnection.Rover_Detected;
                SetLoadingMessage("Rover Connected!", false);
            }
            else
            {
                RoverConnection = RoverConnection.Not_Detected;
                SetLoadingMessage("No Rover Found:(", false);
            }
        }

        private async Task DriveRover(RoverDriverInstructions roverInstruction)
        {
            if (_settings.IsAddingCompleted && _baseUri != null && RoverConnection != RoverConnection.Trying_To_Connect)
            {

                Setting requiredSetting = GetSettingByID((int)roverInstruction);
                Uri instructionUri = null;

                if (requiredSetting != null)
                {
                    try
                    {
                        instructionUri = new Uri(_baseUri, requiredSetting.SettingValue);
                        await _httpClient.GetAsync(instructionUri);
                    }
                    catch (HttpRequestException)
                    {
                        _platformToast.ShowToast($"Request failed to: {instructionUri.ToString()}");
                    }
                }
            }
            else if (_settings.IsAddingCompleted == false) _platformToast.ShowToast("Still Loading Server Endpoints, Try Again.");
            else if (RoverConnection == RoverConnection.Trying_To_Connect) _platformToast.ShowToast("Cannot Drive - Trying to Connect to Rover");

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
                    try
                    {
                        Uri initUri = new Uri(_baseUri, requiredSetting.SettingValue);
                        var response = await _httpClient.GetAsync(initUri);
                    }
                    catch (HttpRequestException)
                    {
                        _platformToast.ShowToast($"Request failed to:{requiredSetting.SettingValue}"); //settings value is base uri tostring value.
                    }
                }
            }
        }

        private async Task SetRoverConnectionStatus()
        {
            if (RoverConnection != RoverConnection.Trying_To_Connect)
            {
                RoverConnection = RoverConnection.Trying_To_Connect;

                if (await _httpClient.HostAvailable(BaseUri))
                {
                    RoverConnection = RoverConnection.Rover_Detected;
                    await InitGPIOs();
                }
                else RoverConnection = RoverConnection.Not_Detected;
            }
        }

        private Setting GetSettingByID(int ID)
        {
            return _settings.Where(x => x.Id == ID).FirstOrDefault();
        }

        private void SetLoadingMessage(string message, bool display)
        {
            LoadingMessage = message;
            LoadingData = display;

        }
    }
}
