using System;
using System.Collections.Generic;
using System.Text;

namespace PiRoverController.Common.Enums
{
    public enum SettingsType
    {
        Wifi = 0,
        Bluetooth = 1
    }
    public enum SettingsIDs //correspond to end point in saved setting
    {
        BaseURL = 1,
        InitGPIOs = 2,
        GoForwards = 3,
        GoBackwards = 4,
        GoLeft = 5,
        GoRight = 6,
        StopFowardsAndBackwards = 7,
        StopLeftAndRight = 8,
        StopAll = 9
    }

}
