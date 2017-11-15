using System;
using Restaurant.Abstractions.Enums;

namespace Restaurant.Abstractions.ViewModels
{
    public interface IMasterViewModel : INavigatableViewModel
	{
		IObservable<NavigationItem> SelectedNavigationItem { get; set; }
	}
}