using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using Restaurant.Abstractions.Enums;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.ViewModels.Android
{
    public class MasterViewModel : ReactiveObject, IMasterViewModel
    {
        public MasterViewModel()
        {   
        }

        private IObservable<NavigationItem> _selectedNavigationItem;
        public IObservable<NavigationItem> SelectedNavigationItem
        {
            get => _selectedNavigationItem;
            set => this.RaiseAndSetIfChanged(ref _selectedNavigationItem, value);
        }

        public string Title => "Menu";
    }
}
