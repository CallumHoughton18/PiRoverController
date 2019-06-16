using PiRoverController.PresentationLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace PiRoverController.PresentationLogic
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected readonly INavigator _navigator;
        public ICommand OnAppearingCommand { get; protected set; }

        public BaseViewModel(INavigator navigator)
        {
            _navigator = navigator;
        }

        public abstract void InitialLoad();

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
