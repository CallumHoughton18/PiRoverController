using System;
using System.Collections.Generic;
using System.Text;
using Ninject;
using PiRoverController.PresentationLogic;
using Xamarin.Forms;

namespace PiRoverController.Factories
{
    public class ViewFactory : IViewFactory
    {
        private readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();
        private readonly IDictionary<Type,Page> _cachedPages = new Dictionary<Type, Page>();
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

        public void RegisterAndCache<ViewModel, View>()
            where ViewModel : BaseViewModel
            where View : Page
        {
            Register<ViewModel, View>();
            var cachedPage = Resolve<ViewModel>();
            _cachedPages.Add(typeof(ViewModel),cachedPage);
        }

        public Page Resolve<ViewModel>() where ViewModel : BaseViewModel
        {
            Page page = null;

            if (_cachedPages.TryGetValue(typeof(ViewModel), out page)) return page;
            var vm = _container.Get<ViewModel>();
            vm.InitialLoad();
            var vmType = typeof(ViewModel);

            Type viewType;
            if (!_map.TryGetValue(vmType, out viewType))
            {
                throw new InvalidOperationException($"No view mapped to {vmType.FullName}");
            }

            page = Activator.CreateInstance(viewType) as Page;
            page.BindingContext = vm;
            return page;
        }
    }
}
