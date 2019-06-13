using System;
using System.Collections.Generic;
using System.Text;
using Ninject;
using PiRoverController.PresentationLogic;
using Xamarin.Forms;

namespace PiRoverController.Factories
{
    class ViewFactory : IViewFactory
    {
        private readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();
        IKernel _container;
        public ViewFactory(IKernel container)
        {
            _container = container;
        }
        public void Register<ViewModel, View>()
            where ViewModel : BaseViewModel
            where View : Page
        {
            _map[typeof(ViewModel)] = typeof(View);
        }

        public Page Resolve<ViewModel>() where ViewModel : BaseViewModel
        {
            var vm = _container.Get<ViewModel>();
            var vmType = typeof(ViewModel);

            Type viewType;
            if (!_map.TryGetValue(vmType, out viewType))
            {
                throw new InvalidOperationException($"No view mapped to {vmType.FullName}");
            }

            var page = Activator.CreateInstance(viewType) as Page;
            page.BindingContext = vm;
            return page;
        }
    }
}
