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
        private readonly object _syncRoot;

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
        public ICommand OnAppearingCommand { get; private set; }

        public SettingsViewModel(ISettingAccess settingAccess, ICommandGenerator commandGenerator, INavigator INavigator) : base(INavigator)
        {
            _settingAccess = settingAccess;
            _commandGenerator = commandGenerator;
            SaveSettingsCommand = _commandGenerator.GenerateCommand(async () => await SaveSettings(Settings));
            OnAppearingCommand = _commandGenerator.GenerateCommand(() =>
            {
                Task.Run(() =>
                {
                    lock (_syncRoot)
                    {
                        LoadSettings();
                    }
                });
            });
        }

        public void LoadSettings()
        {
            Settings = new ObservableCollection<Setting>(_settingAccess.GetSettings());
        }

       private async Task SaveSettings(IEnumerable<Setting> settings)
        {
            foreach (var setting in settings)
            {
                _settingAccess.SaveSetting(setting);
            }

            await _navigator.PopModalAsync();
        }
    }
}
