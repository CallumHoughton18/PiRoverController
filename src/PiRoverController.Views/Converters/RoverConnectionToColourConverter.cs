using PiRoverController.Common.Enums;
using PiRoverController.Views.Styles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PiRoverController.Views
{
    sealed class RoverConnectionToColourConverter : BindableObject, IValueConverter
    {
        public Color SuccessColor
        {
            get { return (Color)base.GetValue(SuccessColorProperty); }
            set { base.SetValue(SuccessColorProperty, value); }
        }
        public static readonly BindableProperty SuccessColorProperty = BindableProperty.Create(nameof(SuccessColor), returnType: typeof(Color), declaringType: typeof(RoverConnectionToColourConverter),
                                                                                                         defaultValue: Color.Black,
                                                                                                         defaultBindingMode: BindingMode.TwoWay);
                                                                                                        
        public Color FailureColor
        {
            get { return (Color)base.GetValue(FailureColorProperty); }
            set { base.SetValue(FailureColorProperty, value); }
        }
        public static readonly BindableProperty FailureColorProperty = BindableProperty.Create(nameof(FailureColor), returnType: typeof(Color), declaringType: typeof(RoverConnectionToColourConverter),
                                                                                                         defaultValue: Color.Black,
                                                                                                         defaultBindingMode: BindingMode.TwoWay);
        public Color InProgressColor
        {
            get { return (Color)base.GetValue(InProgressColorProperty); }
            set { base.SetValue(InProgressColorProperty, value); }
        }
        public static readonly BindableProperty InProgressColorProperty = BindableProperty.Create(nameof(InProgressColor), returnType: typeof(Color), declaringType: typeof(RoverConnectionToColourConverter),
                                                                                                         defaultValue: Color.Black,
                                                                                                         defaultBindingMode: BindingMode.TwoWay);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var connectionStatus = (RoverConnection)value;
            switch (connectionStatus)
            {
                case RoverConnection.Not_Detected:
                    return FailureColor;
                case RoverConnection.Rover_Detected:
                    return SuccessColor;
            }

            return InProgressColor; //return orange when connecting, and in any other scenarios
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
