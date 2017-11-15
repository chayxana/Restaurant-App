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
                .Where(x => x != 0)
                .Select(navigationItemAdapter.GetViewModelFromNavigationItem)
                .Subscribe(async viewModel => await navigationService.NavigateToMainPageContent(viewModel));
        }

        public IMasterViewModel MasterViewModel { get; }
    }
}
