using PiRoverController.PresentationLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PiRoverController.Implementations
{
    sealed class CommandGenerator : ICommandGenerator
    {
        public ICommand GenerateCommand(Action action)
        {
            return new Command(action);
        }
    }
}
