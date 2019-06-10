using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PiRoverController.Common.Interfaces
{
    public interface ICommandGenerator
    {
        ICommand GenerateCommand(Action action);
    }
}
