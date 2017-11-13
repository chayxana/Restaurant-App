using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core.Models;

namespace Restaurant.Core.ViewModels
{
	public class MainViewModel : ReactiveObject, IMainViewModel, IDetailedViewModel
	{
		private readonly INavigationService _navigationService;

		public MainViewModel(
			IMasterViewModel masterViewModel,
			INavigationService navigationService)
		{
			_navigationService = navigationService;
			MasterViewModel = masterViewModel;

			MasterViewModel
				.SelectedMasterItem
				.Where(x => x != null)
				.Subscribe(async masterItem => await _navigationService.NavigateAsync(masterItem.NavigationType));
		}

		[UsedImplicitly]
		public IMasterViewModel MasterViewModel { get; }

		public string Title => "Main";
	}
    
	public interface IDetailedViewModel
	{
		IMasterViewModel MasterViewModel { get; }
	}

	public interface IMasterViewModel
	{
		ObservableCollection<MasterItem> Items { get; }
		IObservable<MasterItem> SelectedMasterItem { get; }
	}
}