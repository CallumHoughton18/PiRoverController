using PiRoverController.Common.Interfaces;
using PiRoverController.Common.Models;
using PiRoverController.PresentationLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PiRoverController.PresentationLogic
{
    public class SettingsViewModel : BaseViewModel
    {
        ISettingAccess _settingAccess;
        ICommandGenerator _commandGenerator;
        IPlatformToast _platformToast;
        private readonly object _syncRoot = new object();

        ObservableCollection<Setting> _settings;
        public ObservableCollection<Setting> Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                if (_settings != value)
                {
                    _settings = value;
                    OnPropertyChanged(nameof(Settings));
                }
            }
        }

        public ICommand SaveSettingsCommand { get; private set; }

        public SettingsViewModel(ISettingAccess settingAccess, ICommandGenerator commandGenerator, INavigator INavigator, IPlatformToast platformToast) : base(INavigator)
        {
            _settingAccess = settingAccess;
            _commandGenerator = commandGenerator;
            _platformToast = platformToast;
            SaveSettingsCommand = _commandGenerator.GenerateCommand(async () =>
            {
                try
                {
                    await SaveSettings(Settings);
                }
                catch (ArgumentNullException)
                {
                    _platformToast.ShowToast("Could not save settings...try again");
                }
            });
        }

        public override void InitialLoad()
        {
            Task.Run(() =>
            {
                lock (_syncRoot)
                {
                    LoadSettings();
                }
            });
        }

        public void LoadSettings()
        {
            Settings = new ObservableCollection<Setting>(_settingAccess.GetSettings());
        }

        private async Task SaveSettings(IEnumerable<Setting> settings)
        {
            if (settings == null) throw new ArgumentNullException("settings", "cannot be null");

            foreach (var setting in settings)
            {
                _settingAccess.SaveSetting(setting);
            }

            await _navigator.PopModalAsync();
        }
    }
}
