using System.Reactive.Linq;
using Autofac;
using ReactiveUI;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core;
using Restaurant.Core.ViewModels;
using Restaurant.Core.ViewModels.Android;
using Xamarin.Forms;
using System;

namespace Restaurant.Mobile.UI.Pages.Android
{
    public class MainPageAndroid : MasterDetailPage, IViewFor<MasterDetailedMainViewModel>
    {
        public MainPageAndroid()
        {
            var container = BootstrapperBase.Container;
            var viewFactory = container.Resolve<IViewFactory>();

            var masterViewModel = container.Resolve<IMasterViewModel>();
            var masterPage = viewFactory.ResolveView(masterViewModel);

            var foodsViewModel = container.Resolve<FoodsViewModel>();
            var foodsPage = viewFactory.ResolveView(foodsViewModel);

            Master = masterPage as Page;
            Detail = new CustomNavigationPage(foodsPage as Page);

            this.WhenAnyValue(x => x.ViewModel.IsNavigated)
                .Where(isNavigated => isNavigated)
                .Subscribe(x =>
                {
                    IsPresented = false;
                    ViewModel.IsNavigated = false;
                });

        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MasterDetailedMainViewModel)value;
        }

        private MasterDetailedMainViewModel _viewModel;
        public MasterDetailedMainViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                OnPropertyChanged();
            }
        }
    }
}