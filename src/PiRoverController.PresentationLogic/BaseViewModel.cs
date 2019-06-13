using PiRoverController.PresentationLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PiRoverController.PresentationLogic
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected readonly INavigator _navigator;

        public BaseViewModel(INavigator navigator)
        {
            _navigator = navigator;
        }

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
