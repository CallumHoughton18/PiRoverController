using System;
using System.Collections.Generic;
using System.Text;

namespace PiRoverController.Common.Enums
{
    public enum RoverDriverInstructions //correspond to end point in saved setting
    {
        GoForwards = SettingsIDs.GoForwards,
        GoBackwards = SettingsIDs.GoBackwards,
        GoLeft = SettingsIDs.GoLeft,
        GoRight = SettingsIDs.GoRight,
        StopFowardsAndBackwards = SettingsIDs.StopFowardsAndBackwards,
        StopLeftAndRight = SettingsIDs.StopLeftAndRight,
        StopAll = SettingsIDs.StopAll
    }
    public enum RoverDirection
    {
        None,
        Left,
        Right,
    }
}
