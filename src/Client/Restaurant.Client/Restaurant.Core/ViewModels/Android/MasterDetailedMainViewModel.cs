using System;
using System.Reactive.Linq;
using ReactiveUI;
using Restaurant.Abstractions.Adapters;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.ViewModels.Android
{
    public class MasterDetailedMainViewModel : MainViewModel, IMasterDetailedViewModel
    {
        public MasterDetailedMainViewModel(
            INavigationService navigationService,
            IMasterViewModel masterViewModel,
            INavigationItemAdapter navigationItemAdapter)
        {
            MasterViewModel = masterViewModel;

            this.WhenAnyValue(x => x.MasterViewModel.SelectedNavigationItem)
                .Where(navigationItem => navigationItem != 0)
                .Select(navigationItemAdapter.GetViewModelFromNavigationItem)
                .Subscribe(async viewModel =>
                {
                    await navigationService.NavigateToMainPageContent(viewModel);
                    IsNavigated = true;
                });
        }

        public IMasterViewModel MasterViewModel { get; }

        private bool _isNavigated;
        public bool IsNavigated
        {
            get => _isNavigated;
            set => this.RaiseAndSetIfChanged(ref _isNavigated, value);
        }
    }
}