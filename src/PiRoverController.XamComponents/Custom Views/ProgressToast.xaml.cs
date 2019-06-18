using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PiRoverController.XamComponents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgressToast : ContentView
    {
        private double _orgYPosition;
        private double _orgXPosition;

        public static readonly BindableProperty ProgressToastTextProperty = BindableProperty.Create("ProgressToastText", returnType: typeof(string), declaringType: typeof(ProgressToast), defaultValue: "",                                                                                        defaultBindingMode: BindingMode.TwoWay, propertyChanged: TextPropertyChanged);
        public string ProgressToastText
        {
            get { return base.GetValue(ProgressToastTextProperty).ToString(); }
            set { base.SetValue(ProgressToastTextProperty, value); }
        }

        public static readonly BindableProperty ProgressToastTextColorProperty = BindableProperty.Create("ProgressToastTextColor", returnType: typeof(Color), declaringType: typeof(ProgressToast), defaultValue: Color.White,
                                                                                              defaultBindingMode: BindingMode.TwoWay, propertyChanged: TextColourPropertyChanged);
        public Color ProgressToastTextColor
        {
            get { return (Color)base.GetValue(ProgressToastTextColorProperty); }
            set { base.SetValue(ProgressToastTextColorProperty, value); }
        }

        public static readonly BindableProperty ProgressToastBackgroundColorProperty = BindableProperty.Create("ProgressToastBackgroundColor", returnType: typeof(Color), declaringType: typeof(ProgressToast), defaultValue: Color.DarkGray,
                                                                                        defaultBindingMode: BindingMode.TwoWay, propertyChanged: BackgroundColourPropertyChanged);
        public Color ProgressToastBackgroundColor
        {
            get { return (Color)base.GetValue(BackgroundColorProperty); }
            set { base.SetValue(BackgroundColorProperty, value); }
        }
        public static readonly BindableProperty ProgressToastBackgroundOpacityProperty = BindableProperty.Create("ProgressToastBackgroundOpacity", returnType: typeof(double), declaringType: typeof(ProgressToast), defaultValue: 0.8D,
                                                                                        defaultBindingMode: BindingMode.TwoWay, propertyChanged: BackgroundOpacityPropertyChanged);

        public Color ProgressToastActivityColor
        {
            get { return (Color)base.GetValue(BackgroundColorProperty); }
            set { base.SetValue(BackgroundColorProperty, value); }
        }
        public static readonly BindableProperty ProgressToastActivityColorProperty = BindableProperty.Create("ProgressToastActivityColor", returnType: typeof(Color), declaringType: typeof(ProgressToast), defaultValue: Color.White,
                                                                                        defaultBindingMode: BindingMode.TwoWay, propertyChanged: ActivityColorChanged);
        public double ProgressToastBackgroundOpacity
        {
            get { return (double)base.GetValue(ProgressToastBackgroundOpacityProperty); }
            set { base.SetValue(ProgressToastBackgroundOpacityProperty, value); }
        }

        public static readonly BindableProperty DisplayToastProperty = BindableProperty.Create("DisplayToast", returnType: typeof(bool), declaringType: typeof(ProgressToast), defaultValue: false,
                                                                                        defaultBindingMode: BindingMode.TwoWay, propertyChanged: DisplayPropertyChanged);

        public bool DisplayToast
        {
            get { return (bool)base.GetValue(DisplayToastProperty); }
            set { base.SetValue(DisplayToastProperty, value); }
        }

        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ProgressToast)bindable;
            control.ProgressToastLabel.Text = newValue.ToString();
        }

        private static void TextColourPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ProgressToast)bindable;
            control.ProgressToastLabel.TextColor = (Color)newValue;
        }

        private static void BackgroundColourPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ProgressToast)bindable;
            control.BackgroundBox.BackgroundColor = (Color)newValue;
        }

        private static void ActivityColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ProgressToast)bindable;
            control.ProgressToastIndicator.Color = (Color)newValue;
        }

        private static void BackgroundOpacityPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ProgressToast)bindable;
            control.BackgroundBox.Opacity = (double)newValue;
        }

        private static void DisplayPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ProgressToast)bindable;
            var val = (bool)newValue;

            Device.BeginInvokeOnMainThread(async () =>
            {

                if (val == true)
                {
                    control.ProgressToastIndicator.IsRunning = true;
                    control.IsVisible = true;
                    await control.TranslateTo(control._orgXPosition, control._orgYPosition + 3, 800);
                }
                else
                {
                    control.ProgressToastIndicator.IsRunning = false;

                    await control.TranslateTo(control._orgXPosition, control._orgYPosition + 1000, 1600);
                    control.IsVisible = false;
                }

            });
        }

        public ProgressToast()
        {
            InitializeComponent();
            _orgXPosition = X;
            _orgYPosition = Y;
            Device.BeginInvokeOnMainThread(async () =>
            {
                this.IsVisible = false;
                await this.TranslateTo(_orgXPosition, _orgYPosition + 1000, 1); //hide and init for pop in animation.s
            });
        }
    }
}