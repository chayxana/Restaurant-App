using System;
using Restaurant.Core.Enums;

namespace Restaurant.Core.ViewModels.Android
{
    public interface IMasterViewModel
	{
		IObservable<NavigationItem> SelectedNavigationItem { get; }
	}
}