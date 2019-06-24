using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PiRoverController.Views.Styles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainStyle : ResourceDictionary
    {
        public MainStyle()
        {
            InitializeComponent();
        }
    }
}