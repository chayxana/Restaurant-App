using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Restaurant.Abstractions.Adapters;
using Restaurant.Abstractions.Api;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.ViewModels
{
	public class FoodsViewModel : BaseViewModel
	{
	    private readonly IBasketViewModel _basketViewModel;
	    private readonly IFoodsApi _foodsApi;
		private readonly INavigationService _navigationService;
		private readonly IFoodDetailViewModelAdapter _foodDetailViewModelAdapter;

		public FoodsViewModel(
			IBasketViewModel basketViewModel,
			IFoodsApi foodsApi,
			INavigationService navigationService,
			IFoodDetailViewModelAdapter foodDetailViewModelAdapter)
		{
		    _basketViewModel = basketViewModel;
		    _foodsApi = foodsApi;
			_navigationService = navigationService;
			_foodDetailViewModelAdapter = foodDetailViewModelAdapter;

            this.WhenAnyValue(x => x.SelectedFood)
				.Where(x => x != null)
				.Subscribe(async food => await NavigateToFoodDetail(food));
		}

	    public async Task LoadFoods()
	    {
	        IsLoading = true;
	        var foods = await _foodsApi.GetFoods();
	        foreach (var food in foods)
	        {
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

		public override string Title => "Foods";

	    // ReSharper disable once ConvertToAutoProperty
	    public IBasketViewModel BasketViewModel => _basketViewModel;

        public ICommand Favorite { get; set; }

		public ICommand AddToBasket { get; set; }

        
    }
}
