using System;
using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.Services;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Factories
{
    public class MainPageFactory : IMainPageFactory
    {
        private readonly IViewResolverService _viewResolverService;

        public MainPageFactory(IViewResolverService viewResolverService)
        {
            _viewResolverService = viewResolverService;
        }

        public IViewFor GetMainPage(INavigatableViewModel vm)
        {
            try
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    return _viewResolverService.ResolveView(vm, Device.Android);
                }

                if (Device.RuntimePlatform == Device.iOS)
                {
                    return _viewResolverService.ResolveView(vm, Device.iOS);
                }

                return _viewResolverService.ResolveView(vm);
            }
            catch (Exception e)
            {
                return null;
            }
           
        }
    }
}
