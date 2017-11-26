using System;
using Autofac;
using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Factories;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Factories
{
    public class ViewFactory : IViewFactory
    {
        private readonly IContainer _container;
		
        public ViewFactory(IContainer container)
        {
            _container = container;
        }

        public IViewFor ResolveView(INavigatableViewModel vm)
        {
	        try
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
	        catch (Exception e)
	        {
		        throw;
	        }
          
        }

        public IViewFor ResolveView(INavigatableViewModel vm, string name)
        {
            var viewType = typeof(IViewFor<>).MakeGenericType(vm.GetType());
            var view = _container.ResolveNamed(name, viewType) as Page;

            if (!(view is IViewFor viewFor))
                throw new Exception(
                    $"Resolve service type '{viewType.FullName}' does not implement '{typeof(IViewFor).FullName}'.");

            view.Title = vm.Title;
            viewFor.ViewModel = vm;
            return viewFor;
        }
    }
}