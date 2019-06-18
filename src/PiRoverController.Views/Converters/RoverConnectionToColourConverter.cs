using PiRoverController.Common.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PiRoverController.Views
{
    sealed class RoverConnectionToColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var connectionStatus = (RoverConnection)value;
            switch (connectionStatus)
            {
                case RoverConnection.Not_Detected:
                    return Color.Red;
                case RoverConnection.Rover_Detected:
                    return Color.Green;
            }

            return Color.Orange; //return orange when connecting, and in any other scenarios
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
