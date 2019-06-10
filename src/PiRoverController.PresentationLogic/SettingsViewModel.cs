using PiRoverController.Common.Interfaces;
using PiRoverController.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace PiRoverController.PresentationLogic
{
    public class SettingsViewModel : BaseViewModel
    {
        public static SettingsViewModel InitializeVM(ISettingAccess settingAccess, ICommandGenerator commandGenerator)
        {
            var vm = new SettingsViewModel(settingAccess, commandGenerator);
            vm.LoadSettings();
            return vm;
        }

        ISettingAccess _settingAccess;
        ICommandGenerator _commandGenerator;

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

        public SettingsViewModel(ISettingAccess settingAccess, ICommandGenerator commandGenerator)
        {
            _settingAccess = settingAccess;
            _commandGenerator = commandGenerator;

            SaveSettingsCommand = _commandGenerator.GenerateCommand(() => SaveSettings(Settings));
        }

        public void LoadSettings()
        {
            Settings = new ObservableCollection<Setting>(_settingAccess.GetSettings());
        }

       private void SaveSettings(IEnumerable<Setting> settings)
        {
            foreach (var setting in settings)
            {
                _settingAccess.SaveSetting(setting);
            }
        }
    }
}
