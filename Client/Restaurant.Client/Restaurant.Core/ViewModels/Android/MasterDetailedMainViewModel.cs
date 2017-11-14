using System;
using System.Collections.Generic;
using System.Text;
using Restaurant.Abstractions.Services;

namespace Restaurant.Core.ViewModels.Android
{
    public class MasterDetailedMainViewModel : MainViewModel, IMasterDetailedViewModel
    {
        public MasterDetailedMainViewModel(INavigationService navigationService)
        {
            //MasterViewModel
            //	.SelectedNavigationItem
            //	.Where(x => x != null)
            //	.Subscribe(async masterItem => await _navigationService.NavigateAsync(masterItem.NavigationType));
        }

        public IMasterViewModel MasterViewModel { get; }
    }
}
