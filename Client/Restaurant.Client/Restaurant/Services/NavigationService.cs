using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Services;
using Xamarin.Forms;

namespace Restaurant.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IContainer _container;
        public INavigation Navigation => App.Current.MainPage.Navigation;

        public NavigationService()
        {
            _container = Bootstrapper.Container;
        }

        public Task NavigateAsync(INavigatableViewModel viewModel)
        {
           return Navigation.PushAsync(null);
        }

        public Task NavigateModalAsync(INavigatableViewModel viewModel)
        {
            return Navigation.PushModalAsync(null);
        }
    }
}
