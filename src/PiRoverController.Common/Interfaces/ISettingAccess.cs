using PiRoverController.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PiRoverController.Common.Interfaces
{
    public interface ISettingAccess
    {
        IEnumerable<Setting> GetSettings();
        Setting RetrieveSetting(string settingName);
        void SaveSetting(Setting setting);
    }
}
