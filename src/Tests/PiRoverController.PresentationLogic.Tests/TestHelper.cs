using PiRoverController.Common.Enums;
using PiRoverController.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiRoverController.PresentationLogic.Tests
{
    public static class TestHelper
    {
        public static IEnumerable<Setting> GetMockSettingsData()
        { 
            //a copy paste of current preset data from datahelper class in .Common. Used here for tests just in case any urls or end points change so test don't have to change.
            return new List<Setting>(){new Setting { Id = (int)SettingsIDs.BaseURL, SettingName = "Web Server IP", SettingValue = "http://192.168.0.22:8080/", SettingType = Common.Enums.SettingsType.Wifi },
                                                   new Setting { Id = (int)SettingsIDs.InitGPIOs, SettingName = "Initialize GPIOs", SettingValue = "/RoverControls/initGPIO", SettingType = Common.Enums.SettingsType.Wifi },
                                                   new Setting { Id = (int)SettingsIDs.GoForwards, SettingName = "Go Forwards Endpoint", SettingValue = "/RoverControls/forward", SettingType = Common.Enums.SettingsType.Wifi },
                                                   new Setting { Id = (int)SettingsIDs.GoBackwards, SettingName = "Go Backwards Endpoint", SettingValue = "/RoverControls/backward", SettingType = Common.Enums.SettingsType.Wifi },
                                                   new Setting { Id = (int)SettingsIDs.GoLeft, SettingName = "Go Left Endpoint", SettingValue = "/RoverControls/left", SettingType = Common.Enums.SettingsType.Wifi },
                                                   new Setting { Id = (int)SettingsIDs.GoRight, SettingName = "Go Right Endpoint", SettingValue = "/RoverControls/right", SettingType = Common.Enums.SettingsType.Wifi },
                                                   new Setting { Id = (int)SettingsIDs.StopLeftAndRight, SettingName = "Stop Left&Right Endpoint", SettingValue = "/RoverControls/stopLeftAndRight", SettingType = Common.Enums.SettingsType.Wifi },
                                                   new Setting { Id = (int)SettingsIDs.StopFowardsAndBackwards, SettingName = "Stop Forwards&Backwards Endpoint", SettingValue = "/RoverControls/stopForwardAndBackward", SettingType = Common.Enums.SettingsType.Wifi } };
        }
    }
}
