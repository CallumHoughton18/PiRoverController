using PiRoverController.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PiRoverController.Common.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
        public SettingsType SettingType { get; set; }
    }
}
