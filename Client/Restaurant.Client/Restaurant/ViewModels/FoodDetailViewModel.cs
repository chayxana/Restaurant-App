using System.Windows.Input;
using ReactiveUI;
using Restaurant.Abstractions.Services;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Models;

namespace Restaurant.ViewModels
{
	public class FoodDetailViewModel : ViewModelBase
	{
		public FoodDetailViewModel(
            FoodDto selectedFood, 
            IBasketViewModel basketViewModel,
            INavigationService navigationService)
		{
			SelectedFood = selectedFood;
			BasketViewModel = basketViewModel;
			CurrentOrder = new OrderViewModel(SelectedFood);

		    CheckOrder = ReactiveCommand.Create(() =>
		    {
                basketViewModel.Orders.Add(CurrentOrder);
		    });

		    GoToBasket = ReactiveCommand.Create(() =>
		    {
		        navigationService.NavigateModalAsync(basketViewModel);
		    });
		}

		public override string Title => SelectedFood.Name;

	    public FoodDto SelectedFood { get; }

		private IBasketViewModel _basketViewModel;
		public IBasketViewModel BasketViewModel
		{
			get => _basketViewModel;
			set => this.RaiseAndSetIfChanged(ref _basketViewModel, value);
		}

		private OrderViewModel _currentOrderViewModel;
	    public OrderViewModel CurrentOrder
	    {
	        get => _currentOrderViewModel;
	        private set => this.RaiseAndSetIfChanged(ref _currentOrderViewModel, value);
	    }

        /// <summary>
        /// Adds current order to BasketViewModel Orders list
        /// </summary>
	    public ICommand CheckOrder { get; }

        /// <summary>
        /// Navigates to Basket ViewModel
        /// </summary>
	    public ICommand GoToBasket { get;  } 
	}
}
