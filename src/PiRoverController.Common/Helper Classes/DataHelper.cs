using PiRoverController.Common.Enums;
using PiRoverController.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiRoverController.Common.Helper_Classes
{
    public static class DataHelper
    {
        public static IEnumerable<Setting> GenerateDefaultSettingsData()
        {
            return new List<Setting>(){new Setting { Id = (int)SettingsIDs.BaseURL, SettingName = "Web Server IP", SettingValue = "http://192.168.0.22:8080", SettingType = SettingsType.Wifi },
                                                   new Setting { Id = (int)SettingsIDs.InitGPIOs, SettingName = "Initialize GPIOs", SettingValue = "/RoverControls/initGPIO/18/16/11/13", SettingType = SettingsType.Wifi },
                                                   new Setting { Id = (int)SettingsIDs.GoForwards, SettingName = "Go Forwards Endpoint", SettingValue = "/RoverControls/forward/18/16", SettingType = SettingsType.Wifi },
                                                   new Setting { Id = (int)SettingsIDs.GoBackwards, SettingName = "Go Backwards Endpoint", SettingValue = "/RoverControls/backward/18/16", SettingType = SettingsType.Wifi },
                                                   new Setting { Id = (int)SettingsIDs.GoLeft, SettingName = "Go Left Endpoint", SettingValue = "/RoverControls/left/11/13", SettingType = SettingsType.Wifi },
                                                   new Setting { Id = (int)SettingsIDs.GoRight, SettingName = "Go Right Endpoint", SettingValue = "/RoverControls/right/11/13", SettingType = SettingsType.Wifi },
                                                   new Setting { Id = (int)SettingsIDs.StopLeftAndRight, SettingName = "Stop Left&Right Endpoint", SettingValue = "/RoverControls/stopLeftAndRight/11/13", SettingType = SettingsType.Wifi },
                                                   new Setting { Id = (int)SettingsIDs.StopFowardsAndBackwards, SettingName = "Stop Forwards&Backwards Endpoint", SettingValue = "/RoverControls/stopForwardAndBackward/18/16", SettingType = SettingsType.Wifi } };
        }
    }
}

/*
GPIO.setup(11, GPIO.OUT) #left
GPIO.setup(13, GPIO.OUT) #right
GPIO.setup(16, GPIO.OUT)#reverse
GPIO.setup(18, GPIO.OUT) #forward
*/