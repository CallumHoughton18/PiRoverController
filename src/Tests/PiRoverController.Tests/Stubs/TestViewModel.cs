using Moq;
using PiRoverController.PresentationLogic;
using PiRoverController.PresentationLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiRoverController.Tests.Stubs
{
    public class TestViewModel : BaseViewModel
    {
        public TestViewModel(INavigator navigator) : base(navigator)
        {

        }
        public override void InitialLoad()
        {
            //throw new NotImplementedException();
        }
    }
}
