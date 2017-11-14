using System;
using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.Services;

namespace Restaurant.Core.Factories
{
    public class MainPageFactory : IMainPageFactory
    {
        private readonly IViewResolverService _viewResolverService;
        private readonly IPlatformFacade _platformFacade;

        public MainPageFactory(
            IViewResolverService viewResolverService,
            IPlatformFacade platformFacade)
        {
            _viewResolverService = viewResolverService;
            _platformFacade = platformFacade;
        }

        public IViewFor GetMainPage(INavigatableViewModel vm)
        {
            try
            {
                if (_platformFacade.RuntimePlatform == _platformFacade.Android)
                {
                    return _viewResolverService.ResolveView(vm, _platformFacade.Android);
                }

                if (_platformFacade.RuntimePlatform == _platformFacade.iOS)
                {
                    return _viewResolverService.ResolveView(vm, _platformFacade.iOS);
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
