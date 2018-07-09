using System.Reactive.Linq;
using Autofac;
using ReactiveUI;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.ViewModels;
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
        }

        public MainPageAndroid(IContainer container)
        {
            var viewFactory = container.Resolve<IViewFactory>();
            var masterPage = viewFactory.ResolveView<IMasterViewModel>() as Page;
            var foodsPage = viewFactory.ResolveView<FoodsViewModel>() as Page;

            Master = masterPage;
            Detail = new CustomNavigationPage(foodsPage) ;

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