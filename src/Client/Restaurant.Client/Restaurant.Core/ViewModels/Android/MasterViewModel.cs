using ReactiveUI;
using Restaurant.Abstractions.Enums;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.ViewModels.Android
{
    public class MasterViewModel : ReactiveObject, IMasterViewModel
    {
        private NavigationItem _selectedNavigationItem;

        public NavigationItem SelectedNavigationItem
        {
            get => _selectedNavigationItem;
            set => this.RaiseAndSetIfChanged(ref _selectedNavigationItem, value);
        }

        public string Title => "Menu";
    }
}