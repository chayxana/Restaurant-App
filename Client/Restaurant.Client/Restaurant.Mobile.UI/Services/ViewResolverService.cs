using System;
using Autofac;
using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Services;
using Restaurant.Core;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Services
{
    public class ViewResolverService : IViewResolverService
    {
        private readonly IContainer _container;

        public ViewResolverService() : this(BootstrapperBase.Container)
        {
        }

        public ViewResolverService(IContainer container)
        {
            _container = container;
        }

        public IViewFor ResolveView(INavigatableViewModel vm)
        {
            var viewType = typeof(IViewFor<>).MakeGenericType(vm.GetType());
            var view = _container.Resolve(viewType) as Page;

            if (!(view is IViewFor ret))
                throw new Exception(
                    $"Resolve service type '{viewType.FullName}' does not implement '{typeof(IViewFor).FullName}'.");

            view.Title = vm.Title;
            ret.ViewModel = vm;
            return ret;
        }

        public IViewFor ResolveView(INavigatableViewModel vm, string name)
        {
            var viewType = typeof(IViewFor<>).MakeGenericType(vm.GetType());
            var view = _container.ResolveNamed(name, viewType) as Page;

            if (!(view is IViewFor ret))
                throw new Exception(
                    $"Resolve service type '{viewType.FullName}' does not implement '{typeof(IViewFor).FullName}'.");

            view.Title = vm.Title;
            ret.ViewModel = vm;
            return ret;
        }
    }
}
