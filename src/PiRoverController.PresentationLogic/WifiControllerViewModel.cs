using PiRoverController.PresentationLogic.Interfaces;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PiRoverController.PresentationLogic
{
    public class WifiControllerViewModel : INotifyPropertyChanged
    {
        private readonly ICommandGenerator _commandGenerator;
        private readonly string entryURL = "http://192.168.0.22:8080/";
        private readonly Uri _baseUri;
        public ICommand GoForwardsCommand { get; private set; }
        public ICommand GoBackwardsCommand { get; private set; }
        public ICommand GoLeftCommand { get; private set; }
        public ICommand GoRightCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        HttpClient _client = new HttpClient();

        public event PropertyChangedEventHandler PropertyChanged;

        public WifiControllerViewModel(ICommandGenerator commandGenerator)
        {
            _baseUri = new Uri(entryURL);
            _commandGenerator = commandGenerator;

            GoForwardsCommand = _commandGenerator.GenerateCommand(async() => await GoForward());
            GoBackwardsCommand = _commandGenerator.GenerateCommand(async () => await GoBackward());
            GoLeftCommand = _commandGenerator.GenerateCommand(async () => await GoLeft());
            GoRightCommand = _commandGenerator.GenerateCommand(async () => await GoRight());
            StopCommand = _commandGenerator.GenerateCommand(async () => await Stop());
        }

        private async Task GoForward()
        {
            Uri forwardUri = new Uri(_baseUri, "/RoverControls/forward");
            await _client.GetAsync(forwardUri);
        }

        private async Task GoBackward()
        {
            Uri forwardUri = new Uri(_baseUri, "/RoverControls/backward");
            await _client.GetAsync(forwardUri);
        }

        private async Task GoLeft()
        {
            Uri forwardUri = new Uri(_baseUri, "/RoverControls/left");
            await _client.GetAsync(forwardUri);
        }

        private async Task GoRight()
        {
            Uri forwardUri = new Uri(_baseUri, "/RoverControls/right");
            await _client.GetAsync(forwardUri);
        }

        private async Task Stop()
        {
            Uri forwardUri = new Uri(_baseUri, "/RoverControls/stop");
            await _client.GetAsync(forwardUri);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
