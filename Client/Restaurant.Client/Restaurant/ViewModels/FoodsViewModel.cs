using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Autofac.Core;
using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Repositories;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Models;
using Splat;
using Restaurant.Abstractions.Services;

namespace Restaurant.ViewModels
{
	public class FoodsViewModel : ReactiveObject, INavigatableViewModel
	{
		private readonly IFoodRepository _foodRepository;
		private readonly INavigationService _navigationService;
		public FoodsViewModel(
			IFoodRepository foodRepository,
			INavigationService navigationService)
		{
			_foodRepository = foodRepository;
			_navigationService = navigationService;
			this.WhenAnyValue(x => x.SelectedFood)
				.Where(x => x != null)
				.Subscribe(async food =>
				{
					var selectedFood = Foods.SingleOrDefault(f => f.Id == food.Id);
					var viewModel = App.Container.Resolve<FoodDetailViewModel>(new NamedParameter("selectedFood", selectedFood));
					await _navigationService.NavigateAsync(viewModel);
				});
		}

		private ObservableCollection<FoodDto> _foods;
		public ObservableCollection<FoodDto> Foods
		{
			get => _foods;
			private set => this.RaiseAndSetIfChanged(ref _foods, value);
		}

		private FoodDto _selectedFood;

		public FoodDto SelectedFood
		{
			get => _selectedFood;
			set => this.RaiseAndSetIfChanged(ref _selectedFood, value);
		}

		public string Title => "Foods";

		public async Task LoadFoods()
		{
			var foods = await _foodRepository.GetAllAsync();
			Foods = new ObservableCollection<FoodDto>(foods);
		}

	}
}
