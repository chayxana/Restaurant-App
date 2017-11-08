using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Restaurant.Abstractions.Adapters;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.ViewModels
{
	public class FoodsViewModel : BaseViewModel
	{
		private readonly IFoodDetailViewModelAdapter _foodDetailViewModelAdapter;
		private readonly IFoodsApi _foodsApi;
		private readonly INavigationService _navigationService;
		private ObservableCollection<FoodDto> _foods;
		private FoodDto _selectedFood;

		public FoodsViewModel(
			IBasketViewModel basketViewModel,
			IFoodsApi foodsApi,
			INavigationService navigationService,
			IFoodDetailViewModelAdapter foodDetailViewModelAdapter)
		{
			_foodsApi = foodsApi;
			_navigationService = navigationService;
			_foodDetailViewModelAdapter = foodDetailViewModelAdapter;

			this.WhenAnyValue(x => x.SelectedFood)
				.Where(x => x != null)
				.Subscribe(async food => await NavigateToFoodDetail(food));

			BasketViewModel = basketViewModel;
		}

		public ObservableCollection<FoodDto> Foods
		{
			get => _foods;
			private set => this.RaiseAndSetIfChanged(ref _foods, value);
		}

		public FoodDto SelectedFood
		{
			get => _selectedFood;
			set => this.RaiseAndSetIfChanged(ref _selectedFood, value);
		}

		public IBasketViewModel BasketViewModel { get; }

		public ICommand Favorite { get; set; }

		public ICommand AddToBasket { get; set; }

		public override string Title => "Foods";

		public async Task LoadFoods()
		{
			IsLoading = true;
			var foods = await _foodsApi.GetFoods();
			if (!Bootstrapper.MockData)
			{
				foreach (var food in foods)
					food.Picture = "http://restaurantserverapi.azurewebsites.net" + food.Picture;
			}
			Foods = new ObservableCollection<FoodDto>(foods);
			IsLoading = false;
		}

		private async Task NavigateToFoodDetail(FoodDto food)
		{
			var viewModel = _foodDetailViewModelAdapter.GetFoodDetailViewModel(food);
			await _navigationService.NavigateAsync(viewModel);
		}
	}
}