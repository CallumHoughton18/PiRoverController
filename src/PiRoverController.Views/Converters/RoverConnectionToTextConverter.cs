using PiRoverController.Common.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PiRoverController.Views
{
    sealed class RoverConnectionToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var connectionStatus = (RoverConnection)value;
            switch (connectionStatus)
            {
                case RoverConnection.Not_Detected:
                    return "No Rover Detected";
                case RoverConnection.Trying_To_Connect:
                    return "Pinging Rover...";
                case RoverConnection.Rover_Detected:
                    return "Rover Connection Established";
            }

            return "Error Determining Connection";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
